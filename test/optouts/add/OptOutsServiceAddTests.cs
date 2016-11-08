using System;
using System.Reflection;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models.response;
using com.esendex.sdk.test.mockapi;
using com.esendex.sdk.test.models.requests.optouts;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.add
{
    [TestFixture]
    public class OptOutsServiceAddTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof (OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private OptOutCreateResult _result;
        private string _phoneNumber;
        private string _accountReference;
        private Guid _optOutId;
        private DateTime _receivedAt;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            _optOutId = Guid.NewGuid();
            _accountReference = "EX0123456";
            _receivedAt = DateTime.UtcNow;
            _phoneNumber = "44123456789";

            var data = new
            {
                OptOut = new
                {
                    Id = _optOutId,
                    ReceivedAt = _receivedAt,
                    AccountReference = _accountReference,
                    From = new
                    {
                        PhoneNumber = _phoneNumber
                    }
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject(data), "application/json; charset=utf-8"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _result = optOutClient.Add(_accountReference, _phoneNumber);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("POST"));
            Assert.That(_request.Url, Is.EqualTo("/v1.0/optouts"));
        }

        [Test]
        public void ThenTheRequestHasTheExpectedHeaders()
        {
            var expectedUserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(expectedUserAgent));

            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
        }

        [Test]
        public void ThenTheCorrectRequestIsSent()
        {
            var request = JsonConvert.DeserializeObject<OptOutsAddRequest>(_request.Body);

            Assert.That(request.AccountReference, Is.EqualTo(_accountReference));
            Assert.That(request.From.PhoneNumber, Is.EqualTo(_phoneNumber));
        }

        [Test]
        public void ThenTheExpectedResultIsReturned()
        {
            Assert.That(_result.OptOut.Id, Is.EqualTo(_optOutId));
            Assert.That(_result.OptOut.AccountReference, Is.EqualTo(_accountReference));
            Assert.That(_result.OptOut.ReceivedAt, Is.EqualTo(_receivedAt).Within(1).Seconds);
            Assert.That(_result.OptOut.From.PhoneNumber, Is.EqualTo(_phoneNumber));

            Assert.That(_result.Errors.Length, Is.EqualTo(0));
        }
    }
}