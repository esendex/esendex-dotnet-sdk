using System;
using System.Net;
using com.esendex.sdk.optouts;
using com.esendex.sdk.test.mockapi;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.getbyid
{
    [TestFixture(403, HttpStatusCode.Forbidden)]
    [TestFixture(404, HttpStatusCode.NotFound)]
    [TestFixture(500, HttpStatusCode.InternalServerError)]
    public class OptOutsServiceGetByIdWithErrorResponseTests
    {
        private readonly int _statusCode;
        private readonly HttpStatusCode _expectedCode;
        private WebException _result;

        public OptOutsServiceGetByIdWithErrorResponseTests(int statusCode, HttpStatusCode expectedCode)
        {
            _statusCode = statusCode;
            _expectedCode = expectedCode;
        }

        [OneTimeSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";
            var optOutId = Guid.NewGuid();

            MockApi.SetEndpoint(new MockEndpoint(_statusCode));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            try
            {
                optOutClient.GetById(optOutId);
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
            Assert.That(webResponse.StatusCode, Is.EqualTo(_expectedCode));
        }
    }
}