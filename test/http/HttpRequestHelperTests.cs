using System;
using System.IO;
using System.Net;
using System.Text;
using com.esendex.sdk.adapters;
using com.esendex.sdk.http;
using NUnit.Framework;
using Moq;

namespace com.esendex.sdk.test.http
{
    [TestFixture]
    public class HttpRequestHelperTests
    {
        private HttpRequestHelper helper;

        private Uri uri;

        [SetUp]
        public void TestInitialize()
        {
            helper = new HttpRequestHelper();
            
            uri = new UriBuilder("http", "tempuri.org").Uri;
        }

        [Test]
        public void Create_WithHttpRequest_ReturnsHttpWebRequestAdapter()
        {
            // Arrange            
            HttpRequest request = new HttpRequest()
            { 
                ResourcePath = "resource",
                HttpMethod = HttpMethod.POST
            };

            string expectedUri = uri.ToString() + "v1.1/" + request.ResourcePath;

            // Act
            IHttpWebRequestAdapter actualHttpRequest = helper.Create(request, uri);

            // Assert
            Assert.That(actualHttpRequest, Is.InstanceOf<HttpWebRequestAdapter>());
            Assert.AreEqual(expectedUri, actualHttpRequest.RequestUri.ToString());
            Assert.AreEqual(request.HttpMethod.ToString(), actualHttpRequest.Method);
            Assert.IsFalse(string.IsNullOrEmpty(actualHttpRequest.UserAgent));
        }

        [Test]
        public void AddCredentials_WithHttpWebRequestAdapterAndBasicEsendexCredentials()
        {
            // Arrange
            EsendexCredentials credentials = new EsendexCredentials("username", "password");

            IHttpWebRequestAdapter httpRequest = new HttpWebRequestAdapter(uri);

            //  Act
            helper.AddCredentials(httpRequest, credentials);

            //  Assert
            string username = httpRequest.Credentials.GetCredential(uri, "Basic").UserName;
            string password = httpRequest.Credentials.GetCredential(uri, "Basic").Password;

            Assert.AreEqual(credentials.Username, username);
            Assert.AreEqual(credentials.Password, password);
        }

        [Test]
        public void AddCredentials_WithHttpWebRequestAdapterAndSessionEsendexCredentials()
        {
            // Arrange
            Guid sessionId = Guid.NewGuid();
            string expectedHeaderValue = string.Format("Basic {0}", Convert.ToBase64String(new UTF8Encoding().GetBytes(sessionId.ToString())));

            EsendexCredentials credentials = new EsendexCredentials(sessionId);

            IHttpWebRequestAdapter httpRequest = new HttpWebRequestAdapter(uri);

            //  Act
            helper.AddCredentials(httpRequest, credentials);

            //  Assert
            string actualHeaderValue = httpRequest.Headers.Get("Authorization");

            Assert.AreEqual(expectedHeaderValue, actualHeaderValue);
        }

        [Test]
        public void AddProxy_WithHttpWebRequestAdapterAndProxy()
        {
            // Arrange
            IHttpWebRequestAdapter httpRequest = new HttpWebRequestAdapter(uri);

            WebProxy proxy = new WebProxy();

            //  Act
            helper.AddProxy(httpRequest, proxy);

            //  Assert
            Assert.AreEqual(proxy, httpRequest.Proxy);
        }

        [Test]
        public void AddContent_WithHttpWebRequestAdapterAndHttpRequest()
        {
            // Arrange
            Mock<IHttpWebRequestAdapter> mockHttpWebRequest = new Mock<IHttpWebRequestAdapter>();

            HttpRequest request = new HttpRequest()
            {
                Content = "content",
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/xml"
            };
            
            Stream stream = new MemoryStream();

            mockHttpWebRequest.Setup(wr => wr.GetRequestStream()).Returns(stream);

            // Act
            helper.AddContent(mockHttpWebRequest.Object, request);

            // Assert
            mockHttpWebRequest.VerifySet(wr => wr.ContentLength = request.ContentLength);
            mockHttpWebRequest.VerifySet(wr => wr.ContentType = request.ContentType);
        }

        [Test]
        public void AddContent_WithHttpWebRequestAdapterAndHttpRequestWithNoContent()
        {
            // Arrange
            Mock<IHttpWebRequestAdapter> mockHttpWebRequest = new Mock<IHttpWebRequestAdapter>();

            HttpRequest request = new HttpRequest()
            {
                Content = string.Empty
            };

            // Act
            helper.AddContent(mockHttpWebRequest.Object, request);

            // Assert
            mockHttpWebRequest.VerifySet(wr => wr.ContentLength = 0);
        }
    }
}