using System.Dynamic;
using System.Xml.Serialization;
using com.esendex.sdk.core;
using com.esendex.sdk.messaging;
using com.esendex.sdk.utilities;
using NUnit.Framework;

namespace com.esendex.sdk.test.messaging
{
    [TestFixture]
    public class SmsMessageSerializationTests
    {
        [Test]
        public void WhenCharacterSetIsDefaultItIsNotSerialised()
        {
            var message = new SmsMessage();
            var serialiser = new XmlSerialiser();
            var serialisedMessage = serialiser.Serialise(message);

            Assert.That(serialisedMessage, Is.Not.StringContaining("characterset"));
        }

        [Test]
        public void WhenCharacterSetIsSetShouldBeSerialised([Values(CharacterSet.Auto, CharacterSet.GSM, CharacterSet.Unicode)] CharacterSet characterSet)
        {
            var message = new SmsMessage();
            message.CharacterSet = characterSet;

            var serialiser = new XmlSerialiser();
            var serialisedMessage = serialiser.Serialise(message);

            Assert.That(serialiser.Deserialise<SmsMessage>(serialisedMessage).CharacterSet, Is.EqualTo(characterSet));
        }
    }
}