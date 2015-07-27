using System;
using System.Net;
using System.Text;
using com.esendex.sdk.adapters;
using com.esendex.sdk.http;
using NUnit.Framework;
using Moq;

namespace com.esendex.sdk.test.http
{
    [TestFixture]
    public class HttpClientTests
    {
        private HttpClient client;

        private Mock<IHttpRequestHelper> httpRequestHelper;
        private Mock<IHttpResponseHelper> httpResponseHelper;
        private Mock<EsendexCredentials> mockEsendexCredentials;

        [SetUp]
        public void TestInitialize()
        {
            httpRequestHelper = new Mock<IHttpRequestHelper>();
            httpResponseHelper = new Mock<IHttpResponseHelper>();
            mockEsendexCredentials = new Mock<EsendexCredentials>();

            var uri = new UriBuilder("http", "tempuri.org").Uri;

            client = new HttpClient(mockEsendexCredentials.Object, uri, httpRequestHelper.Object, httpResponseHelper.Object);
        }

        [Test]
        public void DefaultDIConstructor()
        {
            // Arrange
            var credentials = new EsendexCredentials("username", "password");
            var uri = new Uri("http://tempuri.org");

            var httpRequestHelper = new HttpRequestHelper();
            var httpResponseHelper = new HttpResponseHelper();

            // Act
            var clientInstance = new HttpClient(credentials, uri, httpRequestHelper, httpResponseHelper);

            // Assert
            Assert.That(clientInstance.Credentials, Is.InstanceOf<EsendexCredentials>());
            Assert.That(clientInstance.HttpRequestHelper, Is.InstanceOf<HttpRequestHelper>());
            Assert.That(clientInstance.HttpResponseHelper, Is.InstanceOf<HttpResponseHelper>());
            Assert.That(clientInstance.Uri, Is.InstanceOf<Uri>());
        }

        [Test]
        public void Submit_WithHttpRequest_ReturnsHttpResponse()
        {
            // Arrange
            var request = new HttpRequest
            {
                ResourcePath = "resource",
                HttpMethod = HttpMethod.POST,
                Content = "content",
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/xml"
            };

            var expectedResponse = new HttpResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "content"
            };

            var mockWebRequest = new Mock<IHttpWebRequestAdapter>();
            httpRequestHelper
                .Setup(rh => rh.Create(request, client.Uri, It.IsAny<Version>()))
                .Returns(mockWebRequest.Object);
            httpRequestHelper
                .Setup(rh => rh.AddCredentials(mockWebRequest.Object, mockEsendexCredentials.Object));
            httpRequestHelper
                .Setup(rh => rh.AddProxy(mockWebRequest.Object, mockEsendexCredentials.Object.WebProxy));
            httpRequestHelper
                .Setup(rh => rh.AddContent(mockWebRequest.Object, request));

            var mockWebResponse = new Mock<IHttpWebResponseAdapter>();
            mockWebRequest
                .Setup(wr => wr.GetResponse())
                .Returns(mockWebResponse.Object);

            httpResponseHelper
                .Setup(rh => rh.Create(mockWebResponse.Object))
                .Returns(expectedResponse);

            // Act
            var actualResponse = client.Submit(request);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Submit_GetResponseThrowsWebException_ReturnsHttpResponse()
        {
            // Arrange
            var request = new HttpRequest
            {
                ResourcePath = "http://tempuri.org",
                HttpMethod = HttpMethod.POST,
                Content = "content",
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/xml"
            };

            var webRequest = new Mock<IHttpWebRequestAdapter>();
            httpRequestHelper
                .Setup(rh => rh.Create(request, client.Uri, It.IsAny<Version>()))
                .Returns(webRequest.Object);
            httpRequestHelper
                .Setup(rh => rh.AddCredentials(webRequest.Object, mockEsendexCredentials.Object));
            httpRequestHelper
                .Setup(rh => rh.AddProxy(webRequest.Object, mockEsendexCredentials.Object.WebProxy));
            httpRequestHelper
                .Setup(rh => rh.AddContent(webRequest.Object, request));

            var expectedException = new WebException();
            webRequest
                .Setup(wr => wr.GetResponse())
                .Throws(expectedException);

            var expectedResponse = new HttpResponse
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
            httpResponseHelper
                .Setup(rh => rh.Create(expectedException))
                .Returns(expectedResponse);

            // Act
            var actualResponse = client.Submit(request);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }
    }
}
