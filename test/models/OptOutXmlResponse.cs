using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace com.esendex.sdk.test.models
{
    [XmlRoot("optout", Namespace = Constants.API_NAMESPACE)]
    public class OptOutXmlResponse
    {
        [XmlElement("accountreference")]
        public string AccountReference { get; set; }

        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlElement("receivedat")]
        public DateTime ReceivedAt { get; set; }

        [XmlElement("from")]
        public PhoneNumberResponse FromAddress { get; set; }

        [XmlElement("link")]
        public Link[] Links { get; set; }

        public string SerialiseToXml()
        {
            var serializer = new XmlSerializer(typeof(OptOutXmlResponse));
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, this);
                return stringWriter.ToString();
            }
        }
    }
}