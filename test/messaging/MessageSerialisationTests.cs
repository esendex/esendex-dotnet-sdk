using System.Xml.Linq;
using com.esendex.sdk.messaging;
using com.esendex.sdk.utilities;
using NUnit.Framework;

namespace com.esendex.sdk.test.messaging
{
    [TestFixture]
    public class MessageSerialisationTests
    {
        private readonly XNamespace ns = @"http://api.esendex.com/ns/";

        [Test]
        public void Message_WithoutBlankOriginator_ShouldNotSerialiseOriginator()
        {
            // Arrange
            var message = new SmsMessage
            {
                Originator = string.Empty
            };

            var serialiser = new XmlSerialiser();

            // Act
            var serialisedXml = serialiser.Serialise(message);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element("message")
                                     .Element("from");

            Assert.IsNull(originator);
        }

        [Test]
        public void Message_WithOriginator_SerialisesOriginator()
        {
            // Arrange
            var message = new SmsMessage
            {
                Originator = "07000000000"
            };

            var serialiser = new XmlSerialiser();

            // Act
            var serialisedXml = serialiser.Serialise(message);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element("message")
                                     .Element("from");

            Assert.IsNotNull(originator);
            Assert.AreEqual(message.Originator, originator.Value);
        }

        [Test]
        public void MessageCollection_WithoutBlankOriginator_ShouldNotSerialiseOriginator()
        {
            // Arrange
            var message = new SmsMessageCollection
            {
                Originator = string.Empty
            };

            var serialiser = new XmlSerialiser();

            // Act
            var serialisedXml = serialiser.Serialise(message);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element(ns + "messages")
                                     .Element(ns + "from");

            Assert.IsNull(originator);
        }

        [Test]
        public void MessageCollection_WithOriginator_SerialisesOriginator()
        {
            // Arrange
            var messages = new SmsMessageCollection
            {
                Originator = "07000000000"
            };

            var serialiser = new XmlSerialiser();

            // Act
            var serialisedXml = serialiser.Serialise(messages);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element(ns + "messages")
                                     .Element(ns + "from");

            Assert.IsNotNull(originator);
            Assert.AreEqual(messages.Originator, originator.Value);
        }
    }
}