﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.esendex.sdk.test.models.requests.surveys
{
    public class SurveyAddRecipientsRequest
    {
        [JsonProperty("recipients")]
        public List<SurveyAddRecipientRequest> Recipients { get; set; }
    }
}