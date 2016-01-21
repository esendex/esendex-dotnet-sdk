using System;
using com.esendex.sdk.results;
using com.esendex.sdk.surveys;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.surveys
{
    [TestFixture]
    public class SurveysServiceWithBadRequestTests
    {
        private SurveyResult _result;
        private string _errorValue;
        private string _errorDescription;
        private string _errorCode;
        private string _errorValue2;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";
            const string recipient = "44123456789";
            var surveyId = Guid.NewGuid();

            _errorCode = "survey_problem";
            _errorDescription = "There was a problem";
            _errorValue = "THIS VALUE";
            _errorValue2 = "OTHER VALUE";

            var data = new
            {
                errors = new[]
                {
                    new {code = _errorCode, description = _errorDescription, values = new[] {_errorValue, _errorValue2}}
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(400,
                                                 JsonConvert.SerializeObject(data),
                                                 "application/json; charset=utf-8"));

            var surveysClient = new SurveysService(MockApi.Url, new EsendexCredentials(username, password));

            _result = surveysClient.Send(surveyId, recipient);
        }

        [Test]
        public void ThenTheErrorIsReturned()
        {
            Assert.That(_result.Errors[0].Code, Is.EqualTo(_errorCode));
            Assert.That(_result.Errors[0].Description, Is.EqualTo(_errorDescription));
            Assert.That(_result.Errors[0].Values[0], Is.EqualTo(_errorValue));
            Assert.That(_result.Errors[0].Values[1], Is.EqualTo(_errorValue2));
        }
    }
}