using System.Net;
using com.esendex.sdk.optouts;
using com.esendex.sdk.test.mockapi;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.getall
{
    [TestFixture(403, HttpStatusCode.Forbidden)]
    [TestFixture(404, HttpStatusCode.NotFound)]
    [TestFixture(500, HttpStatusCode.InternalServerError)]
    public class OptOutsServiceGetAllWithErrorTests
    {
        private WebException _result;
        private readonly int _statusCode;
        private readonly HttpStatusCode _expectedCode;

        public OptOutsServiceGetAllWithErrorTests(int statusCode, HttpStatusCode expectedCode)
        {
            _statusCode = statusCode;
            _expectedCode = expectedCode;
        }

        [OneTimeSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            MockApi.SetEndpoint(new MockEndpoint(_statusCode));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            try
            {
                optOutClient.GetAll(1, 2);
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