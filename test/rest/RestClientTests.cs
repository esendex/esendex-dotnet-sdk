using System;
using System.Net;
using System.Text;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using Moq;
using NUnit.Framework;

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

            public PostRestResourceContext(string content) : base(content)
            {
            }
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

            public PutRestResourceContext(string content) : base(content)
            {
            }
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
            var uri = new Uri("http://tempuri.org");
            var credentials = new EsendexCredentials("username", "password");
            IHttpRequestHelper httpRequestHelper = new HttpRequestHelper();
            IHttpResponseHelper httpResponseHelper = new HttpResponseHelper();

            IHttpClient httpClient = new HttpClient(credentials, uri, httpRequestHelper, httpResponseHelper);

            // Act
            var restClientInstance = new RestClient(httpClient);

            // Assert
            Assert.That(restClientInstance.HttpClient, Is.InstanceOf<HttpClient>());
        }

        [Test]
        public void Post_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            var content = "content";

            var postResourceContext = new PostRestResourceContext(content);

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.POST,
                ResourcePath = postResourceContext.ResourcePath,
                Content = postResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            var expectedResponse = new RestResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            var httpResponse = new HttpResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Post(postResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Post_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            var content = "content";

            var postResourceContext = new PostRestResourceContext(content);

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.POST,
                ResourcePath = postResourceContext.ResourcePath,
                Content = postResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Post(postResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }

        [Test]
        public void Get_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            var expectedContent = "content";

            var getResourceContext = new GetRestResourceContext();

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.GET,
                ResourcePath = getResourceContext.ResourcePath,
                ContentType = "text/plain"
            };

            var expectedResponse = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = expectedContent
            };

            var httpResponse = new HttpResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = expectedContent,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8.WebName
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Get(getResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Get_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            var getResourceContext = new GetRestResourceContext();

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.GET,
                ResourcePath = getResourceContext.ResourcePath,
                ContentType = "text/plain"
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Get(getResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }

        [Test]
        public void Put_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            var content = "content";

            var putResourceContext = new PutRestResourceContext(content);

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.PUT,
                ResourcePath = putResourceContext.ResourcePath,
                Content = putResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            var expectedResponse = new RestResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            var httpResponse = new HttpResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Put(putResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Put_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            var content = "content";

            var putResourceContext = new PutRestResourceContext(content);

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.PUT,
                ResourcePath = putResourceContext.ResourcePath,
                Content = putResourceContext.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Put(putResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }

        [Test]
        public void Delete_HttpReturnsIn200Range_ReturnsRestResponse()
        {
            // Arrange
            var deleteResourceContext = new DeleteRestResourceContext();

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.DELETE,
                ResourcePath = deleteResourceContext.ResourcePath,
                ContentType = "text/plain"
            };

            var expectedResponse = new RestResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            var httpResponse = new HttpResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Delete(deleteResourceContext);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
        }

        [Test]
        public void Delete_HttpReturnsNull_ReturnsNull()
        {
            // Arrange
            var deleteResourceContext = new DeleteRestResourceContext();

            var httpRequest = new HttpRequest
            {
                HttpMethod = HttpMethod.DELETE,
                ResourcePath = deleteResourceContext.ResourcePath,
                ContentType = "text/plain"
            };

            HttpResponse httpResponse = null;

            mockHttpClient.Setup(hc => hc.Submit(httpRequest))
                          .Returns(httpResponse);

            // Act
            var actualResponse = restClient.Delete(deleteResourceContext);

            // Assert
            Assert.IsNull(actualResponse);
        }
    }
}