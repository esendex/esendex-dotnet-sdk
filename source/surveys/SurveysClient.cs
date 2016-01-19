using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using com.esendex.sdk.authenticators;
using com.esendex.sdk.models.requests;
using com.esendex.sdk.results;
using Newtonsoft.Json;

namespace com.esendex.sdk.surveys
{
    public class SurveysClient
    {
        private readonly string _baseUrl;
        private readonly IAuthenticator _authenticator;
        private readonly Version _version = Assembly.GetAssembly(typeof (SurveysClient)).GetName().Version;

        public SurveysClient(string baseUrl, IAuthenticator authenticator)
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
            var request = (HttpWebRequest) WebRequest.Create(requestUrl);

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.UserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

            request.Headers.Add("Authorization", _authenticator.Value());

            using (var requestStream = request.GetRequestStream())
            using (var streamWriter = new StreamWriter(requestStream))
            {
                JsonSerializer.Create().Serialize(streamWriter, requestData);
            }

            try
            {
                request.GetResponse();
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse) ex.Response;
                if (response.StatusCode != HttpStatusCode.BadRequest)
                    throw;

                var serializer = new JsonSerializer();
                using (var sr = new StreamReader(response.GetResponseStream()))
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<SurveyResult>(jsonTextReader);
                }
            }

            return new SurveyResult();
        }
    }
}