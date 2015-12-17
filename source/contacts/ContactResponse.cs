using System.Xml.Serialization;

namespace com.esendex.sdk.contacts
{
    [XmlRoot("response", Namespace = Constants.API_NAMESPACE)]
    public class ContactResponse
    {
        [XmlElement("contact")]
        public Contact Contact { get; set; }
    }
}