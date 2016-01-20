using System;
using System.Collections.Generic;
using System.Net;
using com.esendex.sdk.authenticators;
using com.esendex.sdk.extensions;
using com.esendex.sdk.models.requests;
using com.esendex.sdk.results;
using Newtonsoft.Json;

namespace com.esendex.sdk.surveys
{
    public class SurveysClient
    {
        private readonly string _baseUrl;
        private readonly IAuthenticator _authenticator;

        internal SurveysClient(string baseUrl, IAuthenticator authenticator)
        {
            _baseUrl = baseUrl;
            _authenticator = authenticator;
        }

        public SurveyResult AddRecipient(Guid surveyId, string recipient, Dictionary<string, string> templateFields = null)
        {
            var requestData = new SurveysAddRecipientsRequest
            {
                Recipients = new[]
                {
                    new SurveysAddRecipientRequest
                    {
                        PhoneNumber = recipient,
                        TemplateFields = templateFields
                    }
                }
            };

            var requestUrl = string.Format("{0}v1.0/surveys/{1}/send", _baseUrl, surveyId);
            var request = Request.Create("POST", requestUrl)
                                 .WithHeader("Authorization", _authenticator.Value())
                                 .WithAcceptHeader(Constants.JSON_MEDIA_TYPE)
                                 .WriteBody(Constants.JSON_MEDIA_TYPE, streamWriter => JsonSerializer.Create().Serialize(streamWriter, requestData));

            try
            {
                request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse) ex.Response;
                if (response.StatusCode != HttpStatusCode.BadRequest)
                    throw;

                return response.DeserialiseJson<SurveyResult>();
            }

            return new SurveyResult();
        }
    }
}