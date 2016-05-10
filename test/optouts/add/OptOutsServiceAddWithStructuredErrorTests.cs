using System.Linq;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models;
using com.esendex.sdk.optouts.models.response;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.add
{
    [TestFixture]
    public class OptOutsServiceAddWithStructuredErrorTests
    {
        private OptOutCreateResult _result;
        private string _code = "phonenumber_missing";
        private string _description = "Phone number is missing";

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            var data = new
            {
                Errors = new[]
                {
                    new
                    {
                        Code = _code,
                        Description = _description
                    }
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(400, JsonConvert.SerializeObject(data), "application/json"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _result = optOutClient.Add("", "");
        }

        [Test]
        public void ThenTheStructuredErrorIsReturned()
        {
            Assert.That(_result.Errors.Length, Is.EqualTo(1));
            Assert.That(_result.Errors.First().Code, Is.EqualTo(_code));
            Assert.That(_result.Errors.First().Description, Is.EqualTo(_description));

            Assert.That(_result.OptOut, Is.Null);
        }
    }
}