using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace com.esendex.sdk.optouts.models
{
    [XmlRoot("response", Namespace = Constants.API_NAMESPACE)]
    public class OptOutCreateRootResponse
    {
        [XmlElement("optout")]
        public OptOut OptOut { get; set; }

        [XmlElement("errors")]
        public List<ErrorResponse> Errors { get; set; }

        public string SerialiseToXml()
        {
            var serializer = new XmlSerializer(typeof(OptOutCreateRootResponse));
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, this);
                return stringWriter.ToString();
            }
        }
    }

    [Serializable]
    [XmlRoot("optout", Namespace = Constants.API_NAMESPACE)]
    internal class OptOutResponse
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlElement("accountreference")]
        public string AccountReference { get; set; }

        [XmlElement("from")]
        public FromAddress FromAddress { get; set; }

        [XmlElement("link")]
        public Link[] XmlLinks { get; set; }

        [XmlElement("receivedat")]
        public DateTime ReceivedAt { get; set; }
    }
}