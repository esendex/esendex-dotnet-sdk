using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using com.esendex.sdk.http;
using com.esendex.sdk.surveys;
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
            private Uri _expectedUrl;
            private string _expectedUserAgent;

            [TestFixtureSetUp]
            public void WhenGettingTheStandardReport()
            {
                //const string username = "user@example.com";
                //const string password = "heythiscantbeguessed";
                //_surveyId = Guid.NewGuid();
                //_startDate = new DateTime();
                //_endDate = new DateTime();

                //var uriBuilder = HttpUriBuilder.Create(string.Format("v1.0/surveys/{0}/report/standard", _surveyId));
                //uriBuilder.WithParameter("questionSentAfter", _startDate.ToString("O"));
                //uriBuilder.WithParameter("questionSentBefore", _endDate.ToString("O"));

                //_expectedUrl = uriBuilder.Build();
                //_expectedUserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

                //MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject<IEnumerable<StandardReportRow>>(standardReportRows) ,"text/json"));


                //var standardReportEndpoint = new MockEndpoint($"/api/standardreport/{_surveyId}?startdate={HttpUtility.UrlEncode(startDate.ToString("O"))}&enddate={HttpUtility.UrlEncode(endDate.ToString("O"))}&type=answerreceived")
                //                    .WithResponseStatusCode(200)
                //                    .WithResponseBody(JsonConvert.SerializeObject(reportRows))
                //                    .WithContentType("application/json")
                //                    .ForHttpVerb("GET");

                //MockApi.SetEndpoint(standardReportEndpoint);

                //var surveysClient = new SurveySendService(MockApi.Url, new EsendexCredentials(username, password));

                //surveysClient.Send(_surveyId, _recipient);
                //_request = MockApi.LastRequest;
            }

            [Test]
            public void ThenTheStandardReportRowsAreRetreived()
            {
                
            }
        }
    }
}
