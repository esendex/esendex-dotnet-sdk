using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.esendex.sdk.test.models.requests.surveys
{
    public class SurveyAddRecipientRequest
    {
        [JsonProperty("phonenumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("templatefields")]
        public Dictionary<string, string> TemplateFields { get; set; }
    }
}