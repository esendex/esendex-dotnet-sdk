using System;
using System.Net;
using com.esendex.sdk.exceptions;
using com.esendex.sdk.surveys;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.surveys
{
    [TestFixture]
    public class SurveysServiceWithBadRequestTests
    {
        private WebException _result;
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

            try
            {
                surveysClient.Send(surveyId, recipient);
            }
            catch (WebException ex)
            {
                _result = ex;
            }
        }

        [Test]
        public void ThenTheErrorIsReturned()
        {
            var webResponse = (HttpWebResponse)_result.Response;
            Assert.That(webResponse.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void ThenTheExceptionContainsTheErrors()
        {
            var badRequest = (BadRequestException) _result;

            Assert.That(badRequest.Errors[0].Code, Is.EqualTo(_errorCode));
            Assert.That(badRequest.Errors[0].Description, Is.EqualTo(_errorDescription));
            Assert.That(badRequest.Errors[0].Values[0], Is.EqualTo(_errorValue));
            Assert.That(badRequest.Errors[0].Values[1], Is.EqualTo(_errorValue2));
        }
    }
}