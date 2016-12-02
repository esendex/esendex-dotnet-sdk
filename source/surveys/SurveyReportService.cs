using System;
using System.Collections.Generic;
using System.Net;
using com.esendex.sdk.extensions;
using com.esendex.sdk.http;

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
        /// <param name="type">An enum representing what to filter report rows on.</param>
        /// <param name="startDate">A DateTime representing the lower bound for the report date range.</param>
        /// <param name="endDate">A DateTime representing the upper bound for the report date range.</param>
        /// <returns></returns>
        public StandardReportResult GetStandardReport(Guid surveyId, DateRangeType type, DateTime startDate, DateTime endDate)
        {
            var request = Request.Create("GET", GetStandardReportUri(surveyId, type, startDate, endDate))
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

        private Uri GetStandardReportUri(Guid surveyId, DateRangeType type, DateTime startDate, DateTime endDate)
        {
            var requestUrl = _baseUrl + string.Format("v1.0/surveys/{0}/report/standard", surveyId);

            var builder = HttpUriBuilder.Create(requestUrl);

            if (type == DateRangeType.QuestionSent)
            {
                builder.WithParameter("questionSentAfter", startDate.ToString("O"));
                builder.WithParameter("questionSentBefore", endDate.ToString("O"));
            }
            else
            {
                builder.WithParameter("answerReceivedAfter", startDate.ToString("O"));
                builder.WithParameter("answerReceivedBefore", endDate.ToString("O"));
            }

            return builder.Build();
        }

    }

    /// <summary>
    /// A class containing an array of SurveyReportErrors.
    /// </summary>
    public class StandardReportResult
    {
        /// <summary>
        /// An array of StandardReportErrors.
        /// </summary>
        public SurveyReportError[] Errors { get; set; }
        /// <summary>
        /// An array of StandardReportRows.
        /// </summary>
        public StandardReportRow[] Rows { get; set; }
    }

    /// <summary>
    /// Represents a question sent to a recipient and the answer received.
    /// </summary>
    public class StandardReportRow
    {
        /// <summary>
        /// A string containing the phone number of a recipient.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// A string containing the delivery status of the question sent.
        /// </summary>
        public string DeliveryStatus { get; set; }

        /// <summary>
        /// A string containing the label of the question.
        /// </summary>
        public string QuestionLabel { get; set; }

        /// <summary>
        /// A DateTime representing when the question was sent.
        /// </summary>
        public DateTime QuestionDateTime { get; set; }

        /// <summary>
        /// A string containing the label of the answer received.
        /// </summary>
        public string AnswerLabel { get; set; }

        /// <summary>
        /// A nullable DateTime representing when the answer was received.
        /// </summary>
        public DateTime? AnswerDateTime { get; set; }

        /// <summary>
        /// A string containing the recipient's response to the question.
        /// </summary>
        public string AnswerText { get; set; }

        /// <summary>
        /// A dictionary containing both the template fields and metadata saved when adding the recipient to the survey.
        /// </summary>
        public Dictionary<string, string> RecipientData { get; set; }

    }

    /// <summary>
    /// An error containing a code and description.
    /// </summary>
    public class SurveyReportError
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// An enum representing what to filter report rows on.
    /// </summary>
    public enum DateRangeType
    {
        /// <summary>
        /// Filter report rows by the date questions were sent
        /// </summary>
        QuestionSent,

        /// <summary>
        /// Filter report rows by the date answers were received
        /// </summary>
        AnswerReceived
    }
}