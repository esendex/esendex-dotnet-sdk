using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using com.esendex.sdk.core;
using com.esendex.sdk.http;
using com.esendex.sdk.messaging;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;
using Moq;
using NUnit.Framework;

namespace com.esendex.sdk.test.messaging
{
    [TestFixture]
    public class MessagingServiceTests
    {
        private MessagingService service;

        private Mock<ISerialiser> mockSerialiser;
        private Mock<IRestClient> mockRestClient;

        [SetUp]
        public void TestInitialize()
        {
            mockSerialiser = new Mock<ISerialiser>();
            mockRestClient = new Mock<IRestClient>();

            service = new MessagingService(mockRestClient.Object, mockSerialiser.Object, false);
        }

        [Test]
        public void DefaultConstructor()
        {
            // Arrange
            var ensureMessageIdsInResult = false;
            var credentials = new EsendexCredentials("username", "password");

            // Act
            var serviceInstance = new MessagingService(ensureMessageIdsInResult, credentials);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());

            Assert.IsFalse(serviceInstance.EnsureMessageIdsInResult);
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
            var serviceInstance = new MessagingService(restClient, serialiser, true);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());

            Assert.IsTrue(serviceInstance.EnsureMessageIdsInResult);
        }

        [Test]
        public void SendMessage_WithSmsMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var message = new SmsMessage("recipients", "body", "accountReference");
            var messages = new SmsMessageCollection(message);

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendMessage(message);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendMessage_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var message = new VoiceMessage("recipients", "body", "accountReference");
            var messages = new VoiceMessageCollection(message);

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendMessage(message);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendMessages_WithSmsMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var message = new SmsMessage("recipients", "body", "accountReference");
            var messages = new SmsMessageCollection(message);

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendMessages_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var message = new VoiceMessage("recipients", "body", "accountReference");
            var messages = new VoiceMessageCollection(message);

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessage_WithSmsMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var timestamp = DateTime.UtcNow;

            var message = new SmsMessage("recipients", "body", "accountReference");
            var messages = new SmsMessageCollection(message) {SendAt = timestamp};

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendScheduledMessage(message, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessage_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var timestamp = DateTime.UtcNow;

            var message = new VoiceMessage("recipients", "body", "accountReference");
            var messages = new VoiceMessageCollection(message) {SendAt = timestamp};

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendScheduledMessage(message, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessages_WithSmsMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var timestamp = DateTime.UtcNow;

            var message = new SmsMessage("recipients", "body", "accountReference");
            var messages = new SmsMessageCollection(message) {SendAt = timestamp};

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendScheduledMessages(messages, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessages_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            var timestamp = DateTime.UtcNow;

            var message = new VoiceMessage("recipients", "body", "accountReference");
            var messages = new VoiceMessageCollection(message) {SendAt = timestamp};

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            var expectedResult = new MessagingResult
            {
                BatchId = Guid.NewGuid(),
                MessageIds = new List<ResourceLink>()
            };

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<MessagingResult>(response.Content))
                .Returns(expectedResult);

            // Act
            var actualResult = service.SendScheduledMessages(messages, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendSmsMessageCollection_RestClientReturnsNull_ReturnsNull()
        {
            // Arrange
            var message = new SmsMessage("recipients", "body", "accountReference");
            var messages = new SmsMessageCollection(message);

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = null;

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            // Act
            var actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNull(actualResult);
        }

        [Test]
        public void SendVoiceMessageCollection_RestClientReturnsNull_ReturnsNull()
        {
            // Arrange
            var message = new VoiceMessage("recipients", "body", "accountReference");
            var messages = new VoiceMessageCollection(message);

            var serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = null;

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            // Act
            var actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNull(actualResult);
        }
    }
}