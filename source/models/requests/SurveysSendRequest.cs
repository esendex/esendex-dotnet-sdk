using Newtonsoft.Json;

namespace com.esendex.sdk.models.requests
{
    public class SurveysSendRequest
    {
        [JsonProperty("recipients")]
        public SurveysRecipient[] Recipients { get; set; }
    }
}