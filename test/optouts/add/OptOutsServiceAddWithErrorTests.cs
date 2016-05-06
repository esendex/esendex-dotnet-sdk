using System.Net;
using com.esendex.sdk.optouts;
using com.esendex.sdk.test.mockapi;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.add
{
    [TestFixture(403, HttpStatusCode.Forbidden)]
    [TestFixture(409, HttpStatusCode.Conflict)]
    [TestFixture(500, HttpStatusCode.InternalServerError)]
    public class OptOutsServiceAddWithErrorTests
    {
        private WebException _result;
        private readonly int _statusCode;
        private readonly HttpStatusCode _expectedCode;

        public OptOutsServiceAddWithErrorTests(int statusCode, HttpStatusCode expectedCode)
        {
            _statusCode = statusCode;
            _expectedCode = expectedCode;
        }

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            MockApi.SetEndpoint(new MockEndpoint(_statusCode));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            try
            {
                optOutClient.Add("something", "somethingelse");
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