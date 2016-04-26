using System.Xml.Serialization;

namespace com.esendex.sdk.test.models
{
    public class Link
    {
        [XmlAttribute("rel")]
        public string Rel { get; set; }

        [XmlAttribute("href")]
        public string Href { get; set; }
    }
}