using System;
using System.Net;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.session;
using com.esendex.sdk.utilities;
using Moq;
using NUnit.Framework;

namespace com.esendex.sdk.test.session
{
    [TestFixture]
    public class SessionServiceTests
    {
        private SessionService service;

        private Mock<ISerialiser> mockSerialiser;
        private Mock<IRestClient> mockRestClient;

        [SetUp]
        public void TestInitialize()
        {
            mockSerialiser = new Mock<ISerialiser>();
            mockRestClient = new Mock<IRestClient>();

            service = new SessionService(mockRestClient.Object, mockSerialiser.Object);
        }

        [Test]
        public void DefaultConstructor()
        {
            // Arrange
            var credentials = new EsendexCredentials("username", "password");

            // Act
            var serviceInstance = new SessionService(credentials);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());
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

            IRestClient restClient = new RestClient(httpClient);
            ISerialiser serialiser = new XmlSerialiser();

            // Act
            var serviceInstance = new SessionService(restClient, serialiser);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());
        }

        [Test]
        public void CreateSession_ReturnsSessionId()
        {
            // Arrange
            RestResource resource = new SessionResource();

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new EsendexSession
            {
                Id = Guid.NewGuid()
            };

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<EsendexSession>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualSessionId = service.CreateSession();

            // Assert
            Assert.AreEqual(expectedResult.Id, actualSessionId);
        }
    }
}