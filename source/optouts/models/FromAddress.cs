using System.Xml.Serialization;
using Newtonsoft.Json;

namespace com.esendex.sdk.optouts.models
{
    public class FromAddress
    {
        [XmlElement("phonenumber")]
        public string PhoneNumber { get; set; }
    }
}