using System;
using System.Net;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.sent;
using com.esendex.sdk.utilities;
using NUnit.Framework;
using Moq;

namespace com.esendex.sdk.test.sent
{
    [TestFixture]
    public class SentServiceTests
    {
        private SentService service;

        private MockFactory mocks;

        private Mock<ISerialiser> mockSerialiser;
        private Mock<IRestClient> mockRestClient;

        [SetUp]
        public void TestInitialize()
        {
            mocks = new MockFactory(MockBehavior.Strict);

            mockSerialiser = mocks.Create<ISerialiser>();
            mockRestClient = mocks.Create<IRestClient>();

            service = new SentService(mockRestClient.Object, mockSerialiser.Object);
        }

        [TearDown]
        public void TestCleanup()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void DefaultConstructor()
        {
            // Arrange
            EsendexCredentials credentials = new EsendexCredentials("username", "password");

            // Act
            SentService serviceInstance = new SentService(credentials);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());
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

            IRestClient restClient = new RestClient(httpClient);
            ISerialiser serialiser = new XmlSerialiser();

            // Act
            SentService serviceInstance = new SentService(restClient, serialiser);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());
        }

        [Test]
        public void GetMessage_WithId_ReturnsSentMessage()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RestResource resource = new MessageHeadersResource(id);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedItem"
            };

            SentMessage expectedResult = new SentMessage();

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<SentMessage>(response.Content))
                .Returns(expectedResult);

            // Act
            SentMessage actualResult = service.GetMessage(id);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetMessages_WithPageNumberAndPageSize_ReturnsSentMessages()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 15;

            RestResource resource = new MessageHeadersResource(pageNumber, pageSize);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedItem"
            };

            SentMessageCollection expectedResult = new SentMessageCollection()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<SentMessageCollection>(response.Content))
                .Returns(expectedResult);

            // Act
            SentMessageCollection actualResult = service.GetMessages(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(pageNumber, actualResult.PageNumber);
            Assert.AreEqual(pageSize, actualResult.PageSize);
        }

        [Test]
        public void GetMessages_WithPageNumberAndPageSizeAndAccountReference_ReturnsSentMessages()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 15;
            string accountReference = "accountReference";

            RestResource resource = new MessageHeadersResource(accountReference, pageNumber, pageSize);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedItem"
            };

            SentMessageCollection expectedResult = new SentMessageCollection()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<SentMessageCollection>(response.Content))
                .Returns(expectedResult);

            // Act
            SentMessageCollection actualResult = service.GetMessages(accountReference, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(pageNumber, actualResult.PageNumber);
            Assert.AreEqual(pageSize, actualResult.PageSize);
        }
    }
}