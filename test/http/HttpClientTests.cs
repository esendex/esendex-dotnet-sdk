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

        private MockFactory mocks;

        private Mock<IHttpRequestHelper> mockHttpRequestHelper;
        private Mock<IHttpResponseHelper> mockHttpResponseHelper;
        private Mock<EsendexCredentials> mockEsendexCredentials;

        [SetUp]
        public void TestInitialize()
        {
            mocks = new MockFactory(MockBehavior.Strict);

            mockHttpRequestHelper = mocks.Create<IHttpRequestHelper>();
            mockHttpResponseHelper = mocks.Create<IHttpResponseHelper>();
            mockEsendexCredentials = mocks.Create<EsendexCredentials>();

            Uri uri = new UriBuilder("http", "tempuri.org").Uri;

            client = new HttpClient(mockEsendexCredentials.Object, uri, mockHttpRequestHelper.Object, mockHttpResponseHelper.Object);
        }

        [TearDown]
        public void TestCleanup()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void DefaultDIConstructor()
        {
            // Arrange
            EsendexCredentials credentials = new EsendexCredentials("username", "password");
            Uri uri = new Uri("http://tempuri.org");

            IHttpRequestHelper httpRequestHelper = new HttpRequestHelper();
            IHttpResponseHelper httpResponseHelper = new HttpResponseHelper();

            // Act
            HttpClient clientInstance = new HttpClient(credentials, uri, httpRequestHelper, httpResponseHelper);

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
            HttpRequest request = new HttpRequest()
            {
                ResourcePath = "resource",
                HttpMethod = HttpMethod.POST,
                Content = "content",
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/xml"
            };

            HttpResponse expectedResponse = new HttpResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "content"
            };

            Mock<IHttpWebRequestAdapter> mockWebRequest = mocks.Create<IHttpWebRequestAdapter>();
            Mock<IHttpWebResponseAdapter> mockWebResponse = mocks.Create<IHttpWebResponseAdapter>();

            mockHttpRequestHelper
                .Setup(rh => rh.Create(request, client.Uri))
                .Returns(mockWebRequest.Object);

            mockHttpRequestHelper
                .Setup(rh => rh.AddCredentials(mockWebRequest.Object, mockEsendexCredentials.Object));

            mockHttpRequestHelper
                .Setup(rh => rh.AddProxy(mockWebRequest.Object, mockEsendexCredentials.Object.WebProxy));

            mockHttpRequestHelper
                .Setup(rh => rh.AddContent(mockWebRequest.Object, request));

            mockWebRequest
                .Setup(wr => wr.GetResponse())
                .Returns(mockWebResponse.Object);

            mockHttpResponseHelper
                .Setup(rh => rh.Create(mockWebResponse.Object))
                .Returns(expectedResponse);

            // Act
            HttpResponse actualResponse = client.Submit(request);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Submit_GetResponseThrowsWebException_ReturnsHttpResponse()
        {
            // Arrange
            HttpRequest request = new HttpRequest()
            {
                ResourcePath = "http://tempuri.org",
                HttpMethod = HttpMethod.POST,
                Content = "content",
                ContentEncoding = Encoding.UTF8,
                ContentType = "application/xml"
            };

            HttpResponse expectedResponse = new HttpResponse()
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            WebException expectedException = new WebException();

            Mock<IHttpWebRequestAdapter> mockWebRequest = mocks.Create<IHttpWebRequestAdapter>();
            Mock<IHttpWebResponseAdapter> mockWebResponse = mocks.Create<IHttpWebResponseAdapter>();

            mockHttpRequestHelper
                .Setup(rh => rh.Create(request, client.Uri))
                .Returns(mockWebRequest.Object);

            mockHttpRequestHelper
                .Setup(rh => rh.AddCredentials(mockWebRequest.Object, mockEsendexCredentials.Object));

            mockHttpRequestHelper
                .Setup(rh => rh.AddProxy(mockWebRequest.Object, mockEsendexCredentials.Object.WebProxy));

            mockHttpRequestHelper
                .Setup(rh => rh.AddContent(mockWebRequest.Object, request));

            mockWebRequest
                .Setup(wr => wr.GetResponse())
                .Throws(expectedException);

            mockHttpResponseHelper
                .Setup(rh => rh.Create(expectedException))
                .Returns(expectedResponse);

            // Act
            HttpResponse actualResponse = client.Submit(request);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }
    }
}
