using System;
using System.Reflection;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models.response;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.getbyid
{
    [TestFixture]
    public class OptOutsServiceGetByIdTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private OptOut _result;
        private string _phoneNumber;
        private string _accountReference;
        private DateTime _receivedAt;
        private Guid _optOutId;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            _optOutId = Guid.NewGuid();
            _receivedAt = DateTime.UtcNow;
            _accountReference = "EX0123456";
            _phoneNumber = "44123456789";

            var data = new
            {
                Id = _optOutId,
                ReceivedAt = _receivedAt,
                AccountReference = _accountReference,
                From = new
                {
                    PhoneNumber = _phoneNumber
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject(data), "application/json"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _result = optOutClient.GetById(_optOutId);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("GET"));
            Assert.That(_request.Url, Is.EqualTo(string.Format("/v1.0/optouts/{0}", _optOutId)));
        }

        [Test]
        public void ThenTheRequestHasTheExpectedHeaders()
        {
            var expectedUrl = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(expectedUrl));
        }

        [Test]
        public void ThenTheExpectedResultIsReturned()
        {
            Assert.That(_result.Id, Is.EqualTo(_optOutId));
            Assert.That(_result.ReceivedAt, Is.EqualTo(_receivedAt));
            Assert.That(_result.AccountReference, Is.EqualTo(_accountReference));
            Assert.That(_result.From.PhoneNumber, Is.EqualTo(_phoneNumber));
        }
    }
}
