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
    public class OptOutsServiceGetAllWithFromAndAccountReferenceTests
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
        private string _expectedUrl;
        private string _expectedUserAgent;

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
                },
                StartIndex = 0,
                Count = 1,
                TotalCount = 1
            };

            _pageNumber = 1;
            _pageSize = 15;

            _expectedUrl = string.Format("/v1.0/optouts?startIndex={0}&count={1}&accountreference={2}&from={3}", (_pageNumber - 1) * _pageSize, _pageSize, _accountReference, _phoneNumber);
            _expectedUserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

            MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject(data), "application/json"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _result = optOutClient.GetByPhoneNumber(_phoneNumber, _accountReference, _pageNumber, _pageSize);
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
        public void ThenTheExpectedResultIsReturned()
        {
            var optOut = _result.OptOuts.First();
            Assert.That(optOut.Id, Is.EqualTo(_optOutId));
            Assert.That(optOut.AccountReference, Is.EqualTo(_accountReference));
            Assert.That(optOut.ReceivedAt, Is.EqualTo(_receivedAt));
            Assert.That(optOut.FromAddress.PhoneNumber, Is.EqualTo(_phoneNumber));

            Assert.That(_result.PageNumber, Is.EqualTo(1));
            Assert.That(_result.PageSize, Is.EqualTo(1));
            Assert.That(_result.TotalItems, Is.EqualTo(1));
        }
    }
}