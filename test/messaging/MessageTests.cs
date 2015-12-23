using System;
using System.Linq;
using com.esendex.sdk.messaging;
using NUnit.Framework;

namespace com.esendex.sdk.test.messaging
{
    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void SmsMessage_DefaultDIConstructor()
        {
            // Arrange
            var recipients = "recipients";
            var body = "body";
            var accountReference = "accountReference";

            // Act
            var messageInstance = new SmsMessage(recipients, body, accountReference);

            // Assert
            Assert.AreEqual(accountReference, messageInstance.AccountReference);
            Assert.AreEqual(body, messageInstance.Body);
            Assert.AreEqual(recipients, messageInstance.Recipients);
        }

        [Test]
        public void VoiceMessage_DefaultDIConstructor()
        {
            // Arrange
            var recipients = "recipients";
            var body = "body";
            var accountReference = "accountReference";
            var retries = 1;
            var language = VoiceMessageLanguage.en_GB;

            // Act
            var messageInstance = new VoiceMessage(recipients, body, accountReference);

            // Assert
            Assert.AreEqual(accountReference, messageInstance.AccountReference);
            Assert.AreEqual(body, messageInstance.Body);
            Assert.AreEqual(recipients, messageInstance.Recipients);
            Assert.AreEqual(retries, messageInstance.Retries);
            Assert.AreEqual(language, messageInstance.Language);
        }

        [Test]
        public void VoiceMessage_DefaultConstructor()
        {
            // Arrange
            var retries = 1;
            var language = VoiceMessageLanguage.en_GB;

            // Act
            var messageInstance = new VoiceMessage();

            // Assert
            Assert.AreEqual(retries, messageInstance.Retries);
            Assert.AreEqual(language, messageInstance.Language);
        }

        [Test]
        public void SmsMessageCollection_ConstructorWithSmsMessage()
        {
            // Arrange
            var recipients = "recipients";
            var body = "body";
            var accountReference = "accountReference";

            var message = new SmsMessage(recipients, body, accountReference);

            // Act
            var messagesInstance = new SmsMessageCollection(message);

            // Assert
            Assert.AreEqual(message, messagesInstance.Items.ElementAt(0));
        }

        [Test]
        public void VoiceMessageCollection_ConstructorWithVoiceMessage()
        {
            // Arrange
            var recipients = "recipients";
            var body = "body";
            var accountReference = "accountReference";
            var retries = 1;
            var language = VoiceMessageLanguage.en_GB;

            var message = new VoiceMessage(recipients, body, accountReference);

            // Act
            var messagesInstance = new VoiceMessageCollection(message);

            // Assert
            Assert.AreEqual(message, messagesInstance.Items.ElementAt(0));
            Assert.AreEqual(language, messagesInstance.Language);
            Assert.AreEqual(retries, messagesInstance.Retries);
        }

        [Test]
        public void SmsMessageCollection_ConstructorWithSmsMessageArray()
        {
            // Arrange
            var recipients = "recipients";
            var body = "body";
            var accountReference = "accountReference";

            SmsMessage[] messages =
            {
                new SmsMessage(recipients, body, accountReference),
                new SmsMessage(recipients, body, accountReference)
            };

            // Act
            var messagesInstance = new SmsMessageCollection(messages, accountReference);

            // Assert
            Assert.AreEqual(messages[0], messagesInstance.Items.ElementAt(0));
            Assert.AreEqual(messages[1], messagesInstance.Items.ElementAt(1));
        }

        [Test]
        public void VoiceMessageCollection_ConstructorWithVoiceMessageArray()
        {
            // Arrange
            var recipients = "recipients";
            var body = "body";
            var accountReference = "accountReference";
            var retries = 1;
            var language = VoiceMessageLanguage.en_GB;

            VoiceMessage[] messages =
            {
                new VoiceMessage(recipients, body, accountReference),
                new VoiceMessage(recipients, body, accountReference)
            };

            // Act
            var messagesInstance = new VoiceMessageCollection(messages, accountReference);

            // Assert
            Assert.AreEqual(messages[0], messagesInstance.Items.ElementAt(0));
            Assert.AreEqual(messages[1], messagesInstance.Items.ElementAt(1));
            Assert.AreEqual(language, messagesInstance.Language);
            Assert.AreEqual(retries, messagesInstance.Retries);
        }

        [Test]
        public void SmsMessage_DefaultDIConstructor_WithNullOrEmptyParams()
        {
            // Arrange
            var recipients = string.Empty;
            string body = null;
            var accountReference = string.Empty;

            // Act
            try
            {
                var messageInstance = new SmsMessage(recipients, body, accountReference);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("recipients", ex.ParamName);
            }
        }

        [Test]
        public void VoiceMessage_DefaultDIConstructor_WithNullOrEmptyParams()
        {
            // Arrange
            var recipients = string.Empty;
            string body = null;
            var accountReference = string.Empty;

            // Act
            try
            {
                var messageInstance = new VoiceMessage(recipients, body, accountReference);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("recipients", ex.ParamName);
            }
        }

        [Test]
        public void SmsMessageCollection_DefaultDIConstructor_WithNullMessage()
        {
            // Arrange
            SmsMessage message = null;

            // Act
            try
            {
                var messageInstances = new SmsMessageCollection(message);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("message", ex.ParamName);
            }
        }

        [Test]
        public void VoiceMessageCollection_DefaultDIConstructor_WithNullMessage()
        {
            // Arrange
            VoiceMessage message = null;

            // Act
            try
            {
                var messageInstances = new VoiceMessageCollection(message);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("message", ex.ParamName);
            }
        }

        [Test]
        public void SmsMessageCollection_DefaultDIConstructor_WithNullCollectionAndEmptyAccountReference()
        {
            // Arrange

            // Act
            try
            {
                var messageInstances = new SmsMessageCollection(null, string.Empty);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("messages", ex.ParamName);
            }
        }

        [Test]
        public void VoiceMessageCollection_DefaultDIConstructor_WithNullCollectionAndEmptyAccountReference()
        {
            // Arrange

            // Act
            try
            {
                var messageInstances = new VoiceMessageCollection(null, string.Empty);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("messages", ex.ParamName);
            }
        }
    }
}