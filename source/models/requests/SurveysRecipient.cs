using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.esendex.sdk.models.requests
{
    public class SurveysRecipient
    {
        [JsonProperty("phonenumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("templatefields", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> TemplateFields { get; set; }

        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> MetaData { get; set; }
    }
}