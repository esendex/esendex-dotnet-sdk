using System;
using System.Net;
using com.esendex.sdk.authenticators;
using com.esendex.sdk.surveys;
using com.esendex.sdk.test.mockapi;
using NUnit.Framework;

namespace com.esendex.sdk.test.surveys
{
    [TestFixture(403, HttpStatusCode.Forbidden)]
    [TestFixture(404, HttpStatusCode.NotFound)]
    [TestFixture(413, (HttpStatusCode)413)]
    [TestFixture(415, HttpStatusCode.UnsupportedMediaType)]
    [TestFixture(500, HttpStatusCode.InternalServerError)]
    public class SurveysClientWithForbiddenResponseTests
    {
        private readonly int _statusCode;
        private readonly HttpStatusCode _expectedCode;
        private WebException _result;

        public SurveysClientWithForbiddenResponseTests(int statusCode, HttpStatusCode expectedCode)
        {
            _statusCode = statusCode;
            _expectedCode = expectedCode;
        }

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";
            const string recipient = "44123456789";
            var surveyId = Guid.NewGuid();

            MockApi.SetEndpoint(new MockEndpoint(_statusCode));

            var surveysClient = new SurveysClient(MockApi.Url, new BasicAuthenticator(username, password));

            try
            {
                surveysClient.AddRecipient(surveyId, recipient);
            }
            catch (WebException ex)
            {
                _result = ex;
            }
        }

        [Test]
        public void ThenTheErrorIsReturned()
        {
            var webResponse = (HttpWebResponse) _result.Response;
            Assert.That(webResponse.StatusCode, Is.EqualTo(_expectedCode));
        }
    }
}