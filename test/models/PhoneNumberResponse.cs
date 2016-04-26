using System.Xml.Serialization;
using Newtonsoft.Json;

namespace com.esendex.sdk.test.models
{
    public class PhoneNumberResponse
    {
        [JsonProperty("phonenumber")]
        [XmlElement("phonenumber")]
        public string PhoneNumber { get; set; }
    }
}