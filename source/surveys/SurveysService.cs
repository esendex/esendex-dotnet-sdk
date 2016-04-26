using System;
using System.Collections.Generic;
using System.Net;
using com.esendex.sdk.exceptions;
using com.esendex.sdk.extensions;
using com.esendex.sdk.models.requests;
using com.esendex.sdk.results;
using Newtonsoft.Json;

namespace com.esendex.sdk.surveys
{
    public class SurveysService
    {
        private const string SURVEYS_BASE_URL = "https://surveys.api.esendex.com";

        private readonly Uri _baseUrl;
        private readonly EsendexCredentials _credentials;

        internal SurveysService(string baseUrl, EsendexCredentials credentials)
        {
            _baseUrl = new Uri(baseUrl);
            _credentials = credentials;
        }

        public SurveysService(EsendexCredentials credentials) : this(SURVEYS_BASE_URL, credentials) { }
        public SurveysService(string username, string password) : this(SURVEYS_BASE_URL, new EsendexCredentials(username, password)) {}

        public SurveyResult Send(Guid surveyId, string recipient, Dictionary<string, string> templateFields = null)
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
            
            var requestUrl = new Uri(_baseUrl, string.Format("v1.0/surveys/{0}/send", surveyId));
            var request = Request.Create("POST", requestUrl)
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.JSON_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy))
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

                throw new BadRequestException(ex, response.DeserialiseJson<SurveyResult>().Errors);
            }

            return new SurveyResult();
        }
    }
}