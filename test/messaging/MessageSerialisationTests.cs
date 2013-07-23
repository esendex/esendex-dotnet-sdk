using System.Xml.Linq;
using com.esendex.sdk.messaging;
using com.esendex.sdk.utilities;
using NUnit.Framework;

namespace com.esendex.sdk.test.messaging
{
    [TestFixture]
    public class MessageSerialisationTests
    {
        private XNamespace ns = @"http://api.esendex.com/ns/";

        [Test]
        public void Message_WithoutBlankOriginator_ShouldNotSerialiseOriginator()
        {
            // Arrange
            SmsMessage message = new SmsMessage()
            {
                Originator = string.Empty
            };

            XmlSerialiser serialiser = new XmlSerialiser();

            // Act
            string serialisedXml = serialiser.Serialise(message);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element("message").Element("from");

            Assert.IsNull(originator);
        }

        [Test]
        public void Message_WithOriginator_SerialisesOriginator()
        {
            // Arrange
            SmsMessage message = new SmsMessage()
            {
                Originator = "07000000000"
            };

            XmlSerialiser serialiser = new XmlSerialiser();

            // Act
            string serialisedXml = serialiser.Serialise(message);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element("message").Element("from");

            Assert.IsNotNull(originator);
            Assert.AreEqual(message.Originator, originator.Value);
        }

        [Test]
        public void MessageCollection_WithoutBlankOriginator_ShouldNotSerialiseOriginator()
        {
            // Arrange
            SmsMessageCollection message = new SmsMessageCollection()
            {
                Originator = string.Empty
            };

            XmlSerialiser serialiser = new XmlSerialiser();

            // Act
            string serialisedXml = serialiser.Serialise(message);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element(ns + "messages").Element(ns + "from");

            Assert.IsNull(originator);
        }

        [Test]
        public void MessageCollection_WithOriginator_SerialisesOriginator()
        {
            // Arrange
            SmsMessageCollection messages = new SmsMessageCollection()
            {
                Originator = "07000000000"
            };

            XmlSerialiser serialiser = new XmlSerialiser();

            // Act
            string serialisedXml = serialiser.Serialise(messages);

            // Assert
            var document = XDocument.Parse(serialisedXml);

            var originator = document.Element(ns + "messages").Element(ns + "from");

            Assert.IsNotNull(originator);
            Assert.AreEqual(messages.Originator, originator.Value);
        }
    }
}