using com.esendex.sdk.core;
using com.esendex.sdk.messaging;
using com.esendex.sdk.utilities;
using NUnit.Framework;

namespace com.esendex.sdk.test.messaging
{
    [TestFixture]
    public class SmsMessageSerializationTests
    {
        [TestCase(CharacterSet.Auto, @"<?xml version=""1.0"" encoding=""utf-8""?><message><type>SMS</type><validity>0</validity><characterset>Auto</characterset></message>")]
        [TestCase(CharacterSet.GSM, @"<?xml version=""1.0"" encoding=""utf-8""?><message><type>SMS</type><validity>0</validity><characterset>GSM</characterset></message>")]
        [TestCase(CharacterSet.Unicode, @"<?xml version=""1.0"" encoding=""utf-8""?><message><type>SMS</type><validity>0</validity><characterset>Unicode</characterset></message>")]
        [TestCase(CharacterSet.Default, @"<?xml version=""1.0"" encoding=""utf-8""?><message><type>SMS</type><validity>0</validity></message>")]
        public void WhenSmsMessageIsSerializedThenTheCharacterSetIsSerialisedAsExpected(CharacterSet characterSet, string expectedXml)
        {
            var message = new SmsMessage {CharacterSet = characterSet};

            var serialiser = new XmlSerialiser();
            var serialisedMessage = serialiser.Serialise(message);

            Assert.That(serialisedMessage, Is.EqualTo(expectedXml));
        }
    }
}