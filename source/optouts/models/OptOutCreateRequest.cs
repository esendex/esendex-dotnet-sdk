using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.optouts.models
{
    [Serializable]
    [XmlRoot("optout", Namespace = Constants.API_NAMESPACE)]
    public class OptOutCreateRequest
    {
        [XmlElement("accountreference")]
        public string AccountReference { get; set; }

        [XmlElement("from")]
        public FromAddress From { get; set; }
    }
}