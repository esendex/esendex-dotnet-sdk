using System;
using System.Net;
using System.Text;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using NUnit.Framework;
using Moq;

namespace com.esendex.sdk.test.rest
{
    [TestFixture]
    public class RestClientTests
    {
        internal class GetRestResourceContext : RestResource
        {
            public override string ResourceName
            {
                get { return "resource"; }
            }

            public override string ResourceVersion
            {
                get { return "v1.0"; }
            }
        }

        internal class PostRestResourceContext : RestResource
        {
            public override string ResourceName
            {
                get { return "resource"; }
            }

            public override string ResourceVersion
            {
                get { return "v1.2"; }
            }

            public PostRestResourceContext(string content) : base(content) { }
        }

        internal class PutRestResourceContext : RestResource
        {
            public override string ResourceName
            {
                get { return "resource"; }
            }

            public override string ResourceVersion
            {
                get { return "v1.3"; }
            }

            public PutRestResourceContext(string content) : base(content) { }
        }

        internal class DeleteRestResourceContext : RestResource
        {
            public override string ResourceName
            {
                get { return "resource"; }
            }

            public override string ResourceVersion
            {
                get { return "v1.6"; }
            }
        }

        private IRestClient restClient;
        private Mock<IHttpClient> mockHttpClient;

        [SetUp]
        public void TestInitialize()
        {
            mockHttpClient = new Mock<IHttpClient>();
            restClient = new RestClient(mockHttpClient.Object);
        }

        [Test]
        public void DefaultDIConstructor()
        {
            // Arrange
            Uri uri = new Uri("http://tempuri.org");
            EsendexCredentials credentials = new EsendexCredentials("username", "password");
            IHttpRequestHelper httpRequestHelper = new HttpRequestHelper();
            IHttpResponseHelper httpResponseHelper = new HttpResponseHelper();

            IHttpClient httpClient = new HttpClient(credentials, uri, httpRequestHelper, httpResponseHelper);

            // Act
            RestClient restClientInstance = new RestClient(httpClient);

            // Assert
            Assert.That(restClientInstance.HttpClient, Is.InstanceOf<HttpClient>());
        }

        [Test]
        public void Post_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            string content = "content";

            PostRestResourceContext postResourceContext = new PostRestResourceContext(content);

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.POST,
                ResourcePath = postResourceContext.ResourcePath,
                Content = postResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            HttpResponse httpResponse = new HttpResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Post(postResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Post_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            string content = "content";

            PostRestResourceContext postResourceContext = new PostRestResourceContext(content);

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.POST,
                ResourcePath = postResourceContext.ResourcePath,
                Content = postResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.NotFound
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Post(postResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }

        [Test]
        public void Get_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            string expectedContent = "content";

            GetRestResourceContext getResourceContext = new GetRestResourceContext();

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.GET,
                ResourcePath = getResourceContext.ResourcePath,
                ContentType = "text/plain"
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = expectedContent
            };

            HttpResponse httpResponse = new HttpResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = expectedContent,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8.WebName
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Get(getResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Get_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            GetRestResourceContext getResourceContext = new GetRestResourceContext();

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.GET,
                ResourcePath = getResourceContext.ResourcePath,
                ContentType = "text/plain"
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.NotFound
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Get(getResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }

        [Test]
        public void Put_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            string content = "content";

            PutRestResourceContext putResourceContext = new PutRestResourceContext(content);

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.PUT,
                ResourcePath = putResourceContext.ResourcePath,
                Content = putResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            HttpResponse httpResponse = new HttpResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Put(putResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);            
        }

        [Test]
        public void Put_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            string content = "content";

            PutRestResourceContext putResourceContext = new PutRestResourceContext(content);

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.PUT,
                ResourcePath = putResourceContext.ResourcePath,
                Content = putResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.NotFound
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Put(putResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }

        [Test]
        public void Delete_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            DeleteRestResourceContext deleteResourceContext = new DeleteRestResourceContext();

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.DELETE,
                ResourcePath = deleteResourceContext.ResourcePath,
                ContentType = "text/plain",
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            HttpResponse httpResponse = new HttpResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Delete(deleteResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Delete_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            DeleteRestResourceContext deleteResourceContext = new DeleteRestResourceContext();

            HttpRequest httpRequest = new HttpRequest()
            {
                HttpMethod = HttpMethod.DELETE,
                ResourcePath = deleteResourceContext.ResourcePath,
                ContentType = "text/plain"
            };

            RestResponse expectedResponse = new RestResponse()
            {
                StatusCode = HttpStatusCode.NotFound
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest)).Returns(httpResponse);

            // Act
            RestResponse actualResponse = restClient.Delete(deleteResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }
    }
}