using System;
using com.esendex.sdk.surveys;
using com.esendex.sdk.surveys.models;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.surveys.report
{
    [TestFixture]
    public class GivenABadRequestError
    {
        private Guid _surveyId;
        private DateTime _startDate;
        private DateTime _endDate;
        private StandardReportResult _result;
        private StandardReportResult _apiResponse;

        [TestFixtureSetUp]
        public void WhenGettingTheStandardReport()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";
            _surveyId = Guid.NewGuid();
            _startDate = new DateTime(2016, 10, 01);
            _endDate = new DateTime(2016, 10, 25);

            _apiResponse = new StandardReportResult
            {
                Errors = new[]
                {
                    new SurveyReportError {Code = "date_in_future", Description = "Cannot download report for dates in future"}
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(400, JsonConvert.SerializeObject(_apiResponse), "application/json"));

            var surveyReportService = new SurveyReportService(MockApi.Url, new EsendexCredentials(username, password));
            _result = surveyReportService.GetStandardReport(_surveyId, _startDate, _endDate);
        }

        [Test]
        public void ThenTheResultContainsTheErrors()
        {
            Assert.That(_result.Errors[0].Code, Is.EqualTo(_apiResponse.Errors[0].Code));
            Assert.That(_result.Errors[0].Description, Is.EqualTo(_apiResponse.Errors[0].Description));
        }
    }
}
