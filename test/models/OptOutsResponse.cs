using System.IO;
using System.Xml;
using System.Xml.Serialization;
using com.esendex.sdk.test.optouts;

namespace com.esendex.sdk.test.models
{
    [XmlRoot("optouts", Namespace = Constants.API_NAMESPACE)]
    public class OptOutsResponse
    {
        [XmlAttribute("startindex")]
        public int StartIndex { get; set; }

        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlAttribute("totalcount")]
        public int TotalCount { get; set; }

        [XmlElement("optout")]
        public OptOutXmlResponse[] OptOuts { get; set; }

        [XmlElement("link")]
        public Link[] Links { get; set; }

        public string SerialiseToXml()
        {
            var serializer = new XmlSerializer(typeof(OptOutsResponse));
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, this);
                return stringWriter.ToString();
            }
        }
    }
}