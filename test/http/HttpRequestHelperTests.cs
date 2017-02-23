using System;
using System.IO;
using System.Net;
using System.Text;
using com.esendex.sdk.adapters;
using com.esendex.sdk.http;
using Moq;
using NUnit.Framework;

namespace com.esendex.sdk.test.http
{
    [TestFixture]
    public class HttpRequestHelperTests
    {
        private HttpRequestHelper _helper;
        private Uri _uri;

        [SetUp]
        public void TestInitialize()
        {
            _helper = new HttpRequestHelper();

            _uri = new UriBuilder("http", "tempuri.org").Uri;
        }

        [Test]
        public void Create_WithHttpRequest_ReturnsHttpWebRequestAdapter()
        {
            const int major = 1;
            const int minor = 0;
            const int build = 4;
            const int revision = 3;

            // Arrange            
            var request = new HttpRequest
            {
                ResourcePath = "resource",
                HttpMethod = HttpMethod.POST
            };

            var expectedUri = string.Format("{0}v1.1/{1}", _uri, request.ResourcePath);

            var expectedVersion = new Version(major, minor, build, revision);

            // Act
            var actualHttpRequest = _helper.Create(request, _uri, expectedVersion);

            // Assert
            Assert.That(actualHttpRequest, Is.InstanceOf<HttpWebRequestAdapter>());
            Assert.AreEqual(expectedUri, actualHttpRequest.RequestUri.ToString());
            Assert.AreEqual(request.HttpMethod.ToString(), actualHttpRequest.Method);
            Assert.IsFalse(string.IsNullOrEmpty(actualHttpRequest.UserAgent));
            Assert.That(actualHttpRequest.UserAgent, Is.EqualTo(string.Format("Esendex .NET SDK v{0}.{1}.{2}", major, minor, build)));
        }

        [Test]
        public void AddCredentials_WithHttpWebRequestAdapterAndBasicEsendexCredentials()
        {
            // Arrange
            var credentials = new EsendexCredentials("username", "password");
            var expectedHeaderValue = string.Format("Basic {0}", Convert.ToBase64String(new UTF8Encoding().GetBytes($"{credentials.Username}:{credentials.Password}")));

            IHttpWebRequestAdapter httpRequest = new HttpWebRequestAdapter(_uri);

            //  Act
            _helper.AddCredentials(httpRequest, credentials);

            //  Assert
            var actualHeaderValue = httpRequest.Headers.Get("Authorization");

            Assert.AreEqual(expectedHeaderValue, actualHeaderValue);
        }

        [Test]
        public void AddCredentials_WithHttpWebRequestAdapterAndSessionEsendexCredentials()
        {
            // Arrange
            var sessionId = Guid.NewGuid();
            var expectedHeaderValue = string.Format("Basic {0}", Convert.ToBase64String(new UTF8Encoding().GetBytes(sessionId.ToString())));

            var credentials = new EsendexCredentials(sessionId);

            IHttpWebRequestAdapter httpRequest = new HttpWebRequestAdapter(_uri);

            //  Act
            _helper.AddCredentials(httpRequest, credentials);

            //  Assert
            var actualHeaderValue = httpRequest.Headers.Get("Authorization");

            Assert.AreEqual(expectedHeaderValue, actualHeaderValue);
        }

        [Test]
        public void AddProxy_WithHttpWebRequestAdapterAndProxy()
        {
            // Arrange
            IHttpWebRequestAdapter httpRequest = new HttpWebRequestAdapter(_uri);

            var proxy = new WebProxy();

            //  Act
            _helper.AddProxy(httpRequest, proxy);

            //  Assert
            Assert.AreEqual(proxy, httpRequest.Proxy);
        }

        [Test]
        public void AddContent_WithHttpWebRequestAdapterAndHttpRequest()
        {
            // Arrange
            var httpWebRequest = new Mock<IHttpWebRequestAdapter>();

            var request = new HttpRequest
            {
                Content = "content",
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/xml"
            };

            Stream stream = new MemoryStream();

            httpWebRequest.Setup(wr => wr.GetRequestStream())
                          .Returns(stream);

            // Act
            _helper.AddContent(httpWebRequest.Object, request);

            // Assert
            httpWebRequest.VerifySet(wr => wr.ContentLength = request.ContentLength);
            httpWebRequest.VerifySet(wr => wr.ContentType = request.ContentType);
        }

        [Test]
        public void AddContent_WithHttpWebRequestAdapterAndHttpRequestWithNoContent()
        {
            // Arrange
            var httpWebRequest = new Mock<IHttpWebRequestAdapter>();

            var request = new HttpRequest
            {
                Content = string.Empty
            };

            // Act
            _helper.AddContent(httpWebRequest.Object, request);

            // Assert
            httpWebRequest.VerifySet(wr => wr.ContentLength = 0);
        }
    }
}