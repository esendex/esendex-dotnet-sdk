using System;
using System.Net;
using com.esendex.sdk.http;
using com.esendex.sdk.inbox;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;
using NUnit.Framework;
using Moq;

namespace com.esendex.sdk.test.inbox
{
    [TestFixture]
    public class InboxServiceTests
    {
        private InboxService service;

        private Mock<ISerialiser> mockSerialiser;
        private Mock<IRestClient> mockRestClient;

        [SetUp]
        public void TestInitialize()
        {
            mockSerialiser = new Mock<ISerialiser>();
            mockRestClient = new Mock<IRestClient>();

            service = new InboxService(mockRestClient.Object, mockSerialiser.Object);
        }

        [Test]
        public void DefaultConstructor()
        {
            // Arrange
            EsendexCredentials credentials = new EsendexCredentials("username", "password");

            // Act
            InboxService serviceInstance = new InboxService(credentials);

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
            InboxService serviceInstance = new InboxService(restClient, serialiser);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());
        }

        [Test]
        public void GetMessages_ReturnsInboxMessages()
        {
            // Arrange
            RestResource resource = new InboxMessagesResource();

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedItem"
            };

            InboxMessageCollection expectedResult = new InboxMessageCollection();

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<InboxMessageCollection>(response.Content))
                .Returns(expectedResult);

            // Act
            InboxMessageCollection actualResult = service.GetMessages();

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetMessages_WithPageNumberAndPageSize_ReturnsInboxMessages()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 15;

            RestResource resource = new InboxMessagesResource(pageNumber, pageSize);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedItem"
            };

            InboxMessageCollection expectedResult = new InboxMessageCollection()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<InboxMessageCollection>(response.Content))
                .Returns(expectedResult);

            // Act
            InboxMessageCollection actualResult = service.GetMessages(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(pageNumber, actualResult.PageNumber);
            Assert.AreEqual(pageSize, actualResult.PageSize);
        }

        [Test]
        public void GetMessages_WithAccountReference_ReturnsInboxMessages()
        {
            // Arrange
            string accountReference = "accountReference";

            RestResource resource = new InboxMessagesResource(accountReference);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedItem"
            };

            InboxMessageCollection expectedResult = new InboxMessageCollection();

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<InboxMessageCollection>(response.Content))
                .Returns(expectedResult);

            // Act
            InboxMessageCollection actualResult = service.GetMessages(accountReference);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void GetMessages_WithAccountReferenceAndPageNumberAndPageSize_ReturnsInboxMessages()
        {
            // Arrange
            string accountReference = "accountReference";
            int pageNumber = 1;
            int pageSize = 15;

            RestResource resource = new InboxMessagesResource(accountReference, pageNumber, pageSize);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedItem"
            };

            InboxMessageCollection expectedResult = new InboxMessageCollection()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<InboxMessageCollection>(response.Content))
                .Returns(expectedResult);

            // Act
            InboxMessageCollection actualResult = service.GetMessages(accountReference, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedResult.PageNumber, actualResult.PageNumber);
            Assert.AreEqual(expectedResult.PageSize, actualResult.PageSize);
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

            InboxMessage expectedResult = new InboxMessage();

            mockRestClient
                .Setup(rc => rc.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<InboxMessage>(response.Content))
                .Returns(expectedResult);

            // Act
            InboxMessage actualResult = service.GetMessage(id);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void MarkMessageAsRead_WithId_ReturnsTrueWhenSuccessful()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RestResource resource = new InboxMessagesResource(id, InboxMessageStatus.Read);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            mockRestClient
                .Setup(rc => rc.Put(resource))
                .Returns(response);

            // Act
            bool actualResult = service.MarkMessageAsRead(id);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void MarkMessageAsRead_WithId_ReturnsFalseWhenUnsuccessful()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RestResource resource = new InboxMessagesResource(id, InboxMessageStatus.Read);

            RestResponse response = null;

            mockRestClient
                .Setup(rc => rc.Put(resource))
                .Returns(response);

            // Act
            bool actualResult = service.MarkMessageAsRead(id);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void MarkMessageAsUnread_WithId_ReturnsTrueWhenSuccessful()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RestResource resource = new InboxMessagesResource(id, InboxMessageStatus.Unread);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            mockRestClient
                .Setup(rc => rc.Put(resource))
                .Returns(response);

            // Act
            bool actualResult = service.MarkMessageAsUnread(id);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void MarkMessageAsUnread_WithId_ReturnsFalseWhenUnsuccessful()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RestResource resource = new InboxMessagesResource(id, InboxMessageStatus.Unread);

            RestResponse response = null;

            mockRestClient
                .Setup(rc => rc.Put(resource))
                .Returns(response);

            // Act
            bool actualResult = service.MarkMessageAsUnread(id);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void DeleteMessage_WithId_ReturnsTrueWhenSuccessful()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RestResource resource = new InboxMessagesResource(id);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK
            };

            mockRestClient
                .Setup(rc => rc.Delete(resource))
                .Returns(response);

            // Act
            bool actualResult = service.DeleteMessage(id);

            // Assert
            Assert.IsTrue(actualResult);
        }


        [Test]
        public void DeleteMessage_WithId_ReturnsFalseWhenUnsuccessful()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            RestResource resource = new InboxMessagesResource(id);

            RestResponse response = null;

            mockRestClient
                .Setup(rc => rc.Delete(resource))
                .Returns(response);

            // Act
            bool actualResult = service.DeleteMessage(id);

            // Assert
            Assert.IsFalse(actualResult);
        }
    }
}
