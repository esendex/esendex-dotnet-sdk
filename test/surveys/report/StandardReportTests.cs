using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using com.esendex.sdk.surveys;
using com.esendex.sdk.surveys.models;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.surveys.report
{
    [TestFixture]
    public class StandardReportTests
    {
        [TestFixture]
        public class GivenADateRangeForQuestionsSent
        {
            private readonly Version _version = Assembly.GetAssembly(typeof(SurveySendService)).GetName().Version;
            private Guid _surveyId;
            private DateTime _startDate;
            private DateTime _endDate;
            private string _expectedUrl;
            private string _expectedUserAgent;
            private StandardReportResult _result;
            private mockapi.Request _request;
            private StandardReportResult _apiResponse;

            [TestFixtureSetUp]
            public void WhenGettingTheStandardReport()
            {
                const string username = "user@example.com";
                const string password = "heythiscantbeguessed";
                _surveyId = Guid.NewGuid();
                _startDate = new DateTime(2016, 10, 01);
                _endDate = new DateTime(2016, 10, 25);

                _expectedUrl = string.Format("/v1.0/surveys/{0}/report/standard?questionSentAfter={1}&questionSentBefore={2}", _surveyId, HttpUtility.UrlEncode(_startDate.ToString("O")), HttpUtility.UrlEncode(_endDate.ToString("O")));
                _expectedUserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

                _apiResponse = new StandardReportResult
                {
                    Rows = new[]
                    {
                    new StandardReportRow
                    {
                        QuestionDateTime = new DateTime(2016, 10, 10),
                        Recipient = "071234567",
                        AnswerLabel = "1",
                        AnswerDateTime = new DateTime(2016, 10, 11),
                        AnswerText = "Hello",
                        DeliveryStatus = "Delivered",
                        QuestionLabel = "1",
                        RecipientData = new Dictionary<string, string> {{"my key", "my value"}}
                    }
                }
                };

                MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject(_apiResponse), "application/json"));

                var surveyReportService = new SurveyReportService(MockApi.Url, new EsendexCredentials(username, password));
                _result = surveyReportService.GetStandardReport(_surveyId, _startDate, _endDate);

                _request = MockApi.LastRequest;
            }

            [Test]
            public void ThenTheExpectedRequestIsMade()
            {
                Assert.That(_request.Method, Is.EqualTo("GET"));
                Assert.That(_request.Url, Is.EqualTo(_expectedUrl));
            }

            [Test]
            public void ThenTheRequestHasTheExpectedHeaders()
            {
                Assert.That(_request.Headers["Accept"], Is.EqualTo("application/json; charset=utf-8"));
                Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
                Assert.That(_request.Headers["User-Agent"], Is.EqualTo(_expectedUserAgent));
            }

            [Test]
            public void ThenTheStandardReportRowsAreRetrieved()
            {
                Assert.That(_result.Errors, Is.Null);
                Assert.That(_result.Rows.Length, Is.EqualTo(1));
                AssertStandardReportRow(_result.Rows[0], _apiResponse.Rows[0]);
            }

            private static void AssertStandardReportRow(StandardReportRow expected, StandardReportRow result)
            {
                Assert.That(expected.QuestionDateTime, Is.EqualTo(result.QuestionDateTime));
                Assert.That(expected.Recipient, Is.EqualTo(result.Recipient));
                Assert.That(expected.AnswerLabel, Is.EqualTo(result.AnswerLabel));
                Assert.That(expected.AnswerDateTime, Is.EqualTo(result.AnswerDateTime));
                Assert.That(expected.AnswerText, Is.EqualTo(result.AnswerText));
                Assert.That(expected.DeliveryStatus, Is.EqualTo(result.DeliveryStatus));
                Assert.That(expected.QuestionLabel, Is.EqualTo(result.QuestionLabel));
                Assert.That(expected.RecipientData, Is.EqualTo(result.RecipientData));
            }
        }

        [TestFixture]
        public class GivenADateRangeForAnswersReceived
        {
            private readonly Version _version = Assembly.GetAssembly(typeof(SurveySendService)).GetName().Version;
            private Guid _surveyId;
            private DateTime _startDate;
            private DateTime _endDate;
            private string _expectedUrl;
            private string _expectedUserAgent;
            private StandardReportResult _result;
            private mockapi.Request _request;
            private StandardReportResult _apiResponse;

            [TestFixtureSetUp]
            public void WhenGettingTheStandardReport()
            {
                const string username = "user@example.com";
                const string password = "heythiscantbeguessed";
                _surveyId = Guid.NewGuid();
                _startDate = new DateTime(2016, 10, 01);
                _endDate = new DateTime(2016, 10, 25);

                _expectedUrl = string.Format("/v1.0/surveys/{0}/report/standard?answerReceivedAfter={1}&answerReceivedBefore={2}", _surveyId, HttpUtility.UrlEncode(_startDate.ToString("O")), HttpUtility.UrlEncode(_endDate.ToString("O")));
                _expectedUserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

                _apiResponse = new StandardReportResult
                {
                    Rows = new[]
                    {
                    new StandardReportRow
                    {
                        QuestionDateTime = new DateTime(2016, 10, 10),
                        Recipient = "071234567",
                        AnswerLabel = "1",
                        AnswerDateTime = new DateTime(2016, 10, 11),
                        AnswerText = "Hello",
                        DeliveryStatus = "Delivered",
                        QuestionLabel = "1",
                        RecipientData = new Dictionary<string, string> {{"my key", "my value"}}
                    }
                }
                };

                MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject(_apiResponse), "application/json"));

                var surveyReportService = new SurveyReportService(MockApi.Url, new EsendexCredentials(username, password));
                _result = surveyReportService.GetStandardReport(_surveyId, _startDate, _endDate, DateRangeType.AnswerReceived);

                _request = MockApi.LastRequest;
            }

            [Test]
            public void ThenTheExpectedRequestIsMade()
            {
                Assert.That(_request.Method, Is.EqualTo("GET"));
                Assert.That(_request.Url, Is.EqualTo(_expectedUrl));
            }

            [Test]
            public void ThenTheRequestHasTheExpectedHeaders()
            {
                Assert.That(_request.Headers["Accept"], Is.EqualTo("application/json; charset=utf-8"));
                Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
                Assert.That(_request.Headers["User-Agent"], Is.EqualTo(_expectedUserAgent));
            }

            [Test]
            public void ThenTheStandardReportRowsAreRetrieved()
            {
                Assert.That(_result.Errors, Is.Null);
                Assert.That(_result.Rows.Length, Is.EqualTo(1));
                AssertStandardReportRow(_result.Rows[0], _apiResponse.Rows[0]);
            }

            private static void AssertStandardReportRow(StandardReportRow expected, StandardReportRow result)
            {
                Assert.That(expected.QuestionDateTime, Is.EqualTo(result.QuestionDateTime));
                Assert.That(expected.Recipient, Is.EqualTo(result.Recipient));
                Assert.That(expected.AnswerLabel, Is.EqualTo(result.AnswerLabel));
                Assert.That(expected.AnswerDateTime, Is.EqualTo(result.AnswerDateTime));
                Assert.That(expected.AnswerText, Is.EqualTo(result.AnswerText));
                Assert.That(expected.DeliveryStatus, Is.EqualTo(result.DeliveryStatus));
                Assert.That(expected.QuestionLabel, Is.EqualTo(result.QuestionLabel));
                Assert.That(expected.RecipientData, Is.EqualTo(result.RecipientData));
            }
        }

        [TestFixture]
        public class GivenAStartDateForQuestionsSent
        {
            private mockapi.Request _request;
            private Guid _surveyId;
            private DateTime _startDate;

            [TestFixtureSetUp]
            public void WhenGettingTheStandardReport()
            {
                _surveyId = Guid.NewGuid();
                _startDate = new DateTime(2016, 10, 01);
                
                MockApi.SetEndpoint(new MockEndpoint(200, "", "application/json"));

                var surveyReportService = new SurveyReportService(MockApi.Url, new EsendexCredentials("user@example.com", "heythiscantbeguessed"));
                surveyReportService.GetStandardReport(_surveyId, _startDate, null);

                _request = MockApi.LastRequest;
            }

            [Test]
            public void ThenTheExpectedRequestIsMade()
            {
                var expectedUrl = string.Format("/v1.0/surveys/{0}/report/standard?questionSentAfter={1}", _surveyId, HttpUtility.UrlEncode(_startDate.ToString("O")));

                Assert.That(_request.Method, Is.EqualTo("GET"));
                Assert.That(_request.Url, Is.EqualTo(expectedUrl));
            }
        }

        [TestFixture]
        public class GivenAnEndDateForQuestionsSent
        {
            private mockapi.Request _request;
            private Guid _surveyId;
            private DateTime _endDate;

            [TestFixtureSetUp]
            public void WhenGettingTheStandardReport()
            {
                _surveyId = Guid.NewGuid();
                _endDate = new DateTime(2016, 10, 01);

                MockApi.SetEndpoint(new MockEndpoint(200, "", "application/json"));

                var surveyReportService = new SurveyReportService(MockApi.Url, new EsendexCredentials("user@example.com", "heythiscantbeguessed"));
                surveyReportService.GetStandardReport(_surveyId, null, _endDate);

                _request = MockApi.LastRequest;
            }

            [Test]
            public void ThenTheExpectedRequestIsMade()
            {
                var expectedUrl = string.Format("/v1.0/surveys/{0}/report/standard?questionSentBefore={1}", _surveyId, HttpUtility.UrlEncode(_endDate.ToString("O")));

                Assert.That(_request.Method, Is.EqualTo("GET"));
                Assert.That(_request.Url, Is.EqualTo(expectedUrl));
            }
        }

        [TestFixture]
        public class GivenAStartDateForAnswersReceived
        {
            private mockapi.Request _request;
            private Guid _surveyId;
            private DateTime _startDate;

            [TestFixtureSetUp]
            public void WhenGettingTheStandardReport()
            {
                _surveyId = Guid.NewGuid();
                _startDate = new DateTime(2016, 10, 01);

                MockApi.SetEndpoint(new MockEndpoint(200, "", "application/json"));

                var surveyReportService = new SurveyReportService(MockApi.Url, new EsendexCredentials("user@example.com", "heythiscantbeguessed"));
                surveyReportService.GetStandardReport(_surveyId, _startDate, null, DateRangeType.AnswerReceived);

                _request = MockApi.LastRequest;
            }

            [Test]
            public void ThenTheExpectedRequestIsMade()
            {
                var expectedUrl = string.Format("/v1.0/surveys/{0}/report/standard?answerReceivedAfter={1}", _surveyId, HttpUtility.UrlEncode(_startDate.ToString("O")));

                Assert.That(_request.Method, Is.EqualTo("GET"));
                Assert.That(_request.Url, Is.EqualTo(expectedUrl));
            }
        }

        [TestFixture]
        public class GivenAnEndDateForAnswersReceived
        {
            private mockapi.Request _request;
            private Guid _surveyId;
            private DateTime _endDate;

            [TestFixtureSetUp]
            public void WhenGettingTheStandardReport()
            {
                _surveyId = Guid.NewGuid();
                _endDate = new DateTime(2016, 10, 01);

                MockApi.SetEndpoint(new MockEndpoint(200, "", "application/json"));

                var surveyReportService = new SurveyReportService(MockApi.Url, new EsendexCredentials("user@example.com", "heythiscantbeguessed"));
                surveyReportService.GetStandardReport(_surveyId, null, _endDate, DateRangeType.AnswerReceived);

                _request = MockApi.LastRequest;
            }

            [Test]
            public void ThenTheExpectedRequestIsMade()
            {
                var expectedUrl = string.Format("/v1.0/surveys/{0}/report/standard?answerReceivedBefore={1}", _surveyId, HttpUtility.UrlEncode(_endDate.ToString("O")));

                Assert.That(_request.Method, Is.EqualTo("GET"));
                Assert.That(_request.Url, Is.EqualTo(expectedUrl));
            }
        }
    }
}