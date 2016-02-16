using System.Xml.Serialization;

namespace com.esendex.sdk.groups
{
    [XmlRoot("response", Namespace = Constants.API_NAMESPACE)]
    public class GroupResponse
    {
        [XmlElement("contactgroup")]
        public Group Group { get; set; }
    }
}