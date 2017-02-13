using System;
using System.Net;
using com.esendex.sdk.extensions;
using com.esendex.sdk.http;
using com.esendex.sdk.surveys.models;

namespace com.esendex.sdk.surveys
{
    /// <summary>
    /// A service to retrieve reports for a survey
    /// </summary>
    public class SurveyReportService
    {
        private const string SURVEYS_BASE_URL = "https://surveys.api.esendex.com";

        private readonly Uri _baseUrl;
        private readonly EsendexCredentials _credentials;

        internal SurveyReportService(string baseUrl, EsendexCredentials credentials)
        {
            _baseUrl = new Uri(baseUrl);
            _credentials = credentials;
        }

        /// <summary>
        /// Initialises a new instance of the SurveyReportService.
        /// </summary>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance containing your username and password.</param>
        public SurveyReportService(EsendexCredentials credentials) : this(SURVEYS_BASE_URL, credentials) { }

        /// <summary>
        /// Initialises a new instance of the SurveyReportService.
        /// </summary>
        /// <param name="username">A string containing your username.</param>
        /// <param name="password">A string containing your password.</param>
        public SurveyReportService(string username, string password) : this(SURVEYS_BASE_URL, new EsendexCredentials(username, password)) {}

        /// <summary>
        /// Get a standard report for a specified date range and date range type.
        /// </summary>
        /// <param name="surveyId">A System.Guid that contains the Id of an active survey.</param>
        /// <param name="startDate">A DateTime representing the lower bound for the report date range.</param>
        /// <param name="endDate">A DateTime representing the upper bound for the report date range.</param>
        /// <param name="type">An enum representing what to filter report rows on. Defaults to 'QuestionSent'</param>
        /// <returns>A StandardReportResult object containing any errors and the actual report rows</returns>
        public StandardReportResult GetStandardReport(Guid surveyId, DateTime? startDate = null, DateTime? endDate = null, DateRangeType type = DateRangeType.QuestionSent)
        {
            var request = Request.Create("GET", GetStandardReportUri(surveyId, startDate, endDate, type))
                                 .WithHeader("Authorization", "Basic " + _credentials.EncodedValue())
                                 .WithAcceptHeader(Constants.JSON_MEDIA_TYPE)
                                 .If(_credentials.UseProxy, r => r.WithProxy(_credentials.WebProxy));

            HttpWebResponse response;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                if (response.StatusCode != HttpStatusCode.BadRequest)
                    throw;
            }

            return response.DeserialiseJson<StandardReportResult>();
        }

        private Uri GetStandardReportUri(Guid surveyId, DateTime? startDate, DateTime? endDate, DateRangeType? type)
        {
            var requestUrl = _baseUrl + string.Format("v1.0/surveys/{0}/report/standard", surveyId);

            var builder = HttpUriBuilder.Create(requestUrl);

            if (type == DateRangeType.QuestionSent)
            {
                if (startDate != null)
                {
                    builder.WithParameter("questionSentAfter", startDate.Value.ToString("O"));
                }

                if (endDate != null)
                {
                    builder.WithParameter("questionSentBefore", endDate.Value.ToString("O"));
                }
            }
            else
            {
                if (startDate != null)
                {
                    builder.WithParameter("answerReceivedAfter", startDate.Value.ToString("O"));
                }

                if (endDate != null)
                {
                    builder.WithParameter("answerReceivedBefore", endDate.Value.ToString("O"));
                }
            }

            return builder.Build();
        }
    }
}