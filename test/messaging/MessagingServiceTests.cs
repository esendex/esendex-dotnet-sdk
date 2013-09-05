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
using NUnit.Framework;
using Moq;

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
            bool ensureMessageIdsInResult = false;
            EsendexCredentials credentials = new EsendexCredentials("username", "password");
           
            // Act
            MessagingService serviceInstance = new MessagingService(ensureMessageIdsInResult, credentials);
            
            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());

            Assert.IsFalse(serviceInstance.EnsureMessageIdsInResult);
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
            MessagingService serviceInstance = new MessagingService(restClient, serialiser, true);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());

            Assert.IsTrue(serviceInstance.EnsureMessageIdsInResult);
        }

        [Test]
        public void SendMessage_WithSmsMessage_ReturnsBatchIdResult()
        { 
            // Arrange
            SmsMessage message = new SmsMessage("recipients", "body", "accountReference");
            SmsMessageCollection messages = new SmsMessageCollection(message);

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };
            
            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendMessage(message);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendMessage_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            VoiceMessage message = new VoiceMessage("recipients", "body", "accountReference");
            VoiceMessageCollection messages = new VoiceMessageCollection(message);

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendMessage(message);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendMessages_WithSmsMessage_ReturnsBatchIdResult()
        {
            // Arrange
            SmsMessage message = new SmsMessage("recipients", "body", "accountReference");
            SmsMessageCollection messages = new SmsMessageCollection(message);

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendMessages_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            VoiceMessage message = new VoiceMessage("recipients", "body", "accountReference");
            VoiceMessageCollection messages = new VoiceMessageCollection(message);

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessage_WithSmsMessage_ReturnsBatchIdResult()
        {
            // Arrange
            DateTime timestamp = DateTime.UtcNow;

            SmsMessage message = new SmsMessage("recipients", "body", "accountReference");
            SmsMessageCollection messages = new SmsMessageCollection(message) { SendAt = timestamp };

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendScheduledMessage(message, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessage_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            DateTime timestamp = DateTime.UtcNow;

            VoiceMessage message = new VoiceMessage("recipients", "body", "accountReference");
            VoiceMessageCollection messages = new VoiceMessageCollection(message) { SendAt = timestamp };

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendScheduledMessage(message, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessages_WithSmsMessage_ReturnsBatchIdResult()
        {
            // Arrange
            DateTime timestamp = DateTime.UtcNow;

            SmsMessage message = new SmsMessage("recipients", "body", "accountReference");
            SmsMessageCollection messages = new SmsMessageCollection(message) { SendAt = timestamp };

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendScheduledMessages(messages, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendScheduledMessages_WithVoiceMessage_ReturnsBatchIdResult()
        {
            // Arrange
            DateTime timestamp = DateTime.UtcNow;

            VoiceMessage message = new VoiceMessage("recipients", "body", "accountReference");
            VoiceMessageCollection messages = new VoiceMessageCollection(message) { SendAt = timestamp };

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = new RestResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = "serialisedResponse"
            };

            MessagingResult expectedResult = new MessagingResult()
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
            MessagingResult actualResult = service.SendScheduledMessages(messages, timestamp);

            // Assert
            Assert.IsNotNull(actualResult);
            Assert.AreEqual(expectedResult.BatchId, actualResult.BatchId);
            Assert.AreEqual(0, actualResult.MessageIds.Count());
        }

        [Test]
        public void SendSmsMessageCollection_RestClientReturnsNull_ReturnsNull()
        {
            // Arrange
            SmsMessage message = new SmsMessage("recipients", "body", "accountReference");
            SmsMessageCollection messages = new SmsMessageCollection(message);

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = null;

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            // Act
            MessagingResult actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNull(actualResult);
        }

        [Test]
        public void SendVoiceMessageCollection_RestClientReturnsNull_ReturnsNull()
        {
            // Arrange
            VoiceMessage message = new VoiceMessage("recipients", "body", "accountReference");
            VoiceMessageCollection messages = new VoiceMessageCollection(message);

            string serialisedMessage = "serialisedMessage";

            RestResource resource = new MessageDispatcherResource(serialisedMessage, false);

            RestResponse response = null;

            mockSerialiser
                .Setup(s => s.Serialise(messages))
                .Returns(serialisedMessage);

            mockRestClient
                .Setup(rc => rc.Post(resource))
                .Returns(response);

            // Act
            MessagingResult actualResult = service.SendMessages(messages);

            // Assert
            Assert.IsNull(actualResult);
        }
    }
}