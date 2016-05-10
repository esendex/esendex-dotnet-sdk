using System;
using System.Linq;
using System.Reflection;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models.response;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.getall
{
    [TestFixture]
    public class OptOutsServiceGetAllWithFromTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private OptOutCollection _result;
        private string _phoneNumber;
        private string _accountReference;
        private int _pageNumber;
        private int _pageSize;
        private Guid _optOutId;
        private DateTime _receivedAt;

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
                OptOuts = new[]
                {
                    new
                    {
                        Id = _optOutId,
                        ReceivedAt = _receivedAt,
                        AccountReference = _accountReference,
                        FromAddress = new { PhoneNumber = _phoneNumber }
                    }
                }
            };

            _pageNumber = 1;
            _pageSize = 15;

            MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject(data), "application/json"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _result = optOutClient.GetByFromAddress(_phoneNumber, _pageNumber, _pageSize);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            var expectedUrl = string.Format("/v1.0/optouts?startIndex={0}&count={1}&from={2}", (_pageNumber - 1) * _pageSize, _pageSize, _phoneNumber);

            Assert.That(_request.Method, Is.EqualTo("GET"));
            Assert.That(_request.Url, Is.EqualTo(expectedUrl));
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
        public void ThenTheExpectedResultIsReturned()
        {
            var optOut = _result.OptOuts.First();
            Assert.That(optOut.Id, Is.EqualTo(_optOutId));
            Assert.That(optOut.AccountReference, Is.EqualTo(_accountReference));
            Assert.That(optOut.ReceivedAt, Is.EqualTo(_receivedAt));
            Assert.That(optOut.FromAddress.PhoneNumber, Is.EqualTo(_phoneNumber));
        }
    }
}