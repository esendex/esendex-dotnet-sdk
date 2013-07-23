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
            string recipients = "recipients";
            string body = "body";
            string accountReference = "accountReference";

            // Act
            SmsMessage messageInstance = new SmsMessage(recipients, body, accountReference);

            // Assert
            Assert.AreEqual(accountReference, messageInstance.AccountReference);
            Assert.AreEqual(body, messageInstance.Body);
            Assert.AreEqual(recipients, messageInstance.Recipients);
        }

        [Test]
        public void VoiceMessage_DefaultDIConstructor()
        {
            // Arrange
            string recipients = "recipients";
            string body = "body";
            string accountReference = "accountReference";
            int retries = 1;
            VoiceMessageLanguage language = VoiceMessageLanguage.en_GB;

            // Act
            VoiceMessage messageInstance = new VoiceMessage(recipients, body, accountReference);

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
            int retries = 1;
            VoiceMessageLanguage language = VoiceMessageLanguage.en_GB;

            // Act
            VoiceMessage messageInstance = new VoiceMessage();

            // Assert
            Assert.AreEqual(retries, messageInstance.Retries);
            Assert.AreEqual(language, messageInstance.Language);
        }

        [Test]
        public void SmsMessageCollection_ConstructorWithSmsMessage()
        {
            // Arrange
            string recipients = "recipients";
            string body = "body";
            string accountReference = "accountReference";

            SmsMessage message = new SmsMessage(recipients, body, accountReference);

            // Act
            SmsMessageCollection messagesInstance = new SmsMessageCollection(message);

            // Assert
            Assert.AreEqual(message, messagesInstance.Items.ElementAt(0));
        }

        [Test]
        public void VoiceMessageCollection_ConstructorWithVoiceMessage()
        {
            // Arrange
            string recipients = "recipients";
            string body = "body";
            string accountReference = "accountReference";
            int retries = 1;
            VoiceMessageLanguage language = VoiceMessageLanguage.en_GB;

            VoiceMessage message = new VoiceMessage(recipients, body, accountReference);

            // Act
            VoiceMessageCollection messagesInstance = new VoiceMessageCollection(message);

            // Assert
            Assert.AreEqual(message, messagesInstance.Items.ElementAt(0));
            Assert.AreEqual(language, messagesInstance.Language);
            Assert.AreEqual(retries, messagesInstance.Retries);
        }

        [Test]
        public void SmsMessageCollection_ConstructorWithSmsMessageArray()
        {
            // Arrange
            string recipients = "recipients";
            string body = "body";
            string accountReference = "accountReference";

            SmsMessage[] messages = new SmsMessage[]
            {
                new SmsMessage(recipients, body, accountReference),
                new SmsMessage(recipients, body, accountReference)
            };

            // Act
            SmsMessageCollection messagesInstance = new SmsMessageCollection(messages, accountReference);

            // Assert
            Assert.AreEqual(messages[0], messagesInstance.Items.ElementAt(0));
            Assert.AreEqual(messages[1], messagesInstance.Items.ElementAt(1));
        }

        [Test]
        public void VoiceMessageCollection_ConstructorWithVoiceMessageArray()
        {
            // Arrange
            string recipients = "recipients";
            string body = "body";
            string accountReference = "accountReference";
            int retries = 1;
            VoiceMessageLanguage language = VoiceMessageLanguage.en_GB;

            VoiceMessage[] messages = new VoiceMessage[]
            {
                new VoiceMessage(recipients, body, accountReference),
                new VoiceMessage(recipients, body, accountReference)
            };

            // Act
            VoiceMessageCollection messagesInstance = new VoiceMessageCollection(messages, accountReference);

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
            string recipients = string.Empty;
            string body = null;
            string accountReference = string.Empty;

            // Act
            try
            {
                SmsMessage messageInstance = new SmsMessage(recipients, body, accountReference);

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
            string recipients = string.Empty;
            string body = null;
            string accountReference = string.Empty;

            // Act
            try
            {
                VoiceMessage messageInstance = new VoiceMessage(recipients, body, accountReference);

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
                SmsMessageCollection messageInstances = new SmsMessageCollection(message);

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
                VoiceMessageCollection messageInstances = new VoiceMessageCollection(message);

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
                SmsMessageCollection messageInstances = new SmsMessageCollection(null, string.Empty);

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
                VoiceMessageCollection messageInstances = new VoiceMessageCollection(null, string.Empty);

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
