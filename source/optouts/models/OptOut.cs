using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.optouts.models
{
    [Serializable]
    [XmlRoot("optout", Namespace = Constants.API_NAMESPACE)]
    public class OptOut
    {
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        [XmlElement("accountreference")]
        public string AccountReference { get; set; }

        [XmlElement("from")]
        public FromAddress FromAddress { get; set; }

        [XmlElement("link")]
        public Link[] Links { get; set; }

        [XmlElement("receivedat")]
        public DateTime ReceivedAt { get; set; }
    }
}