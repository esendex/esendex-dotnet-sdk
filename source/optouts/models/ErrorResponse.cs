using System.Xml.Serialization;

namespace com.esendex.sdk.optouts.models
{
    public class ErrorResponse
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("link")]
        public Link[] Links { get; set; }
    }
}