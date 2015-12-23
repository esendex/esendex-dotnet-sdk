using System;
using com.esendex.sdk.core;
using com.esendex.sdk.messaging;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;
using Moq;
using NUnit.Framework;

namespace com.esendex.sdk.test.messaging
{
    [TestFixture]
    public class MessageBodyServiceTests
    {
        private Mock<ISerialiser> mockSerialiser;
        private Mock<IRestClient> mockRestClient;
        private MessageBodyService service;

        [SetUp]
        public void TestInitialize()
        {
            mockSerialiser = new Mock<ISerialiser>();
            mockRestClient = new Mock<IRestClient>();

            service = new MessageBodyService(mockRestClient.Object, mockSerialiser.Object);
        }

        [Test]
        public void LoadBodyText_UpdatesMessageBodyWithBodyText()
        {
            const string expectedMessageBody = "This is the body of the message";

            var id = Guid.NewGuid();
            var messageBody = new MessageBody
            {
                Id = id,
                Uri = string.Format("https://api.esendex.com/v1.0/messageheaders/{0}/body", id),
                BodyText = string.Empty
            };

            mockRestClient
                .Setup(c => c.Get(It.IsAny<ResourceLinkResource>()))
                .Returns(new RestResponse());
            mockSerialiser
                .Setup(s => s.Deserialise<MessageBody>(It.IsAny<string>()))
                .Returns(new MessageBody {BodyText = expectedMessageBody});

            service.LoadBodyText(messageBody);

            Assert.That(messageBody.BodyText, Is.EqualTo(expectedMessageBody));
        }
    }
}