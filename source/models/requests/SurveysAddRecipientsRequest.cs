using Newtonsoft.Json;

namespace com.esendex.sdk.models.requests
{
    public class SurveysAddRecipientsRequest
    {
        [JsonProperty("recipients")]
        public SurveysAddRecipientRequest[] Recipients { get; set; }
    }
}