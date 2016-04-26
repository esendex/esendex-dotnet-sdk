using System;
using System.Linq;
using System.Reflection;
using com.esendex.sdk.core;
using com.esendex.sdk.optouts;
using com.esendex.sdk.test.mockapi;
using com.esendex.sdk.test.models;
using NUnit.Framework;
using Link = com.esendex.sdk.test.models.Link;

namespace com.esendex.sdk.test.optouts
{
    [TestFixture]
    public class OptOutsServiceGetAllWithFromAndAccountReferenceTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private SubscriptionCollection _result;
        private OptOutsResponse _optOutsResponse;
        private string _phoneNumber;
        private string _accountReference;
        private int _pageNumber;
        private int _pageSize;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            var optOutId = Guid.NewGuid();
            _phoneNumber = "44123456789";
            _accountReference = "EX0123456";
            _optOutsResponse = new OptOutsResponse
            {
                OptOuts = new []
                {
                    new OptOutXmlResponse
                    {
                        Id = optOutId,
                        ReceivedAt = DateTime.UtcNow,
                        AccountReference = _accountReference,
                        FromAddress = new PhoneNumberResponse { PhoneNumber = _phoneNumber },
                        Links = new [] { new Link{ Rel = "self", Href = "https://api.esendex.com/v1.0/" + optOutId } }
                     }
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(200, _optOutsResponse.SerialiseToXml(), "application/xml"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _pageNumber = 1;
            _pageSize = 15;
            _result = optOutClient.GetByPhoneNumber(_phoneNumber, _accountReference, _pageNumber, _pageSize);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("GET"));
            Assert.That(_request.Url, Is.EqualTo(string.Format("/v1.0/optouts?accountreference={0}&from={1}&startIndex={2}&count={3}", _accountReference, _phoneNumber, (--_pageNumber)*_pageSize, _pageSize)));
        }

        [Test]
        public void ThenTheRequestHasTheExpectedHeaders()
        {
            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/xml; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build)));
        }

        [Test]
        public void ThenTheExpectedResultIsReturned()
        {
            Assert.That(_result.OptOuts.First().Id, Is.EqualTo(_optOutsResponse.OptOuts.First().Id));
            Assert.That(_result.OptOuts.First().AccountReference, Is.EqualTo(_optOutsResponse.OptOuts.First().AccountReference));
            Assert.That(_result.OptOuts.First().ReceivedAt, Is.EqualTo(_optOutsResponse.OptOuts.First().ReceivedAt));
            Assert.That(_result.OptOuts.First().FromAddress.PhoneNumber, Is.EqualTo(_optOutsResponse.OptOuts.First().FromAddress.PhoneNumber));
            Assert.That(_result.OptOuts.First().Links.First(o => o.Rel == "self").Href, Is.EqualTo(_optOutsResponse.OptOuts.First().Links.First().Href));
        }
    }

    [TestFixture]
    public class OptOutsServiceGetAllTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private SubscriptionCollection _result;
        private OptOutsResponse _optOutsResponse;
        private string _phoneNumber;
        private string _accountReference;
        private int _pageSize;
        private int _pageNumber;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            var optOutId = Guid.NewGuid();
            _phoneNumber = "44123456789";
            _accountReference = "EX0123456";
            _optOutsResponse = new OptOutsResponse
            {
                OptOuts = new[]
                {
                    new OptOutXmlResponse
                    {
                        Id = optOutId,
                        ReceivedAt = DateTime.UtcNow,
                        AccountReference = _accountReference,
                        FromAddress = new PhoneNumberResponse { PhoneNumber = _phoneNumber },
                        Links = new [] { new Link{ Rel = "self", Href = "https://api.esendex.com/v1.0/" + optOutId } }
                     }
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(200, _optOutsResponse.SerialiseToXml(), "application/xml"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _pageSize = 15;
            _pageNumber = 1;
            _result = optOutClient.GetAll(_pageNumber, _pageSize);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("GET"));
            Assert.That(_request.Url, Is.EqualTo(string.Format("/v1.0/optouts?startIndex={0}&count={1}", (--_pageNumber) * _pageSize, _pageSize)));
        }

        [Test]
        public void ThenTheRequestHasTheExpectedHeaders()
        {
            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/xml; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build)));
        }

        [Test]
        public void ThenTheExpectedResultIsReturned()
        {
            Assert.That(_result.OptOuts.First().Id, Is.EqualTo(_optOutsResponse.OptOuts.First().Id));
            Assert.That(_result.OptOuts.First().AccountReference, Is.EqualTo(_optOutsResponse.OptOuts.First().AccountReference));
            Assert.That(_result.OptOuts.First().ReceivedAt, Is.EqualTo(_optOutsResponse.OptOuts.First().ReceivedAt));
            Assert.That(_result.OptOuts.First().FromAddress.PhoneNumber, Is.EqualTo(_optOutsResponse.OptOuts.First().FromAddress.PhoneNumber));
            Assert.That(_result.OptOuts.First().Links.First(o => o.Rel == "self").Href, Is.EqualTo(_optOutsResponse.OptOuts.First().Links.First().Href));
        }
    }

    [TestFixture]
    public class OptOutsServiceGetAllWithFromTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private SubscriptionCollection _result;
        private OptOutsResponse _optOutsResponse;
        private string _phoneNumber;
        private string _accountReference;
        private int _pageNumber;
        private int _pageSize;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            var optOutId = Guid.NewGuid();
            _phoneNumber = "44123456789";
            _accountReference = "EX0123456";
            _optOutsResponse = new OptOutsResponse
            {
                OptOuts = new []
                {
                    new OptOutXmlResponse
                    {
                        Id = optOutId,
                        ReceivedAt = DateTime.UtcNow,
                        AccountReference = _accountReference,
                        FromAddress = new PhoneNumberResponse { PhoneNumber = _phoneNumber },
                        Links = new [] { new Link{ Rel = "self", Href = "https://api.esendex.com/v1.0/" + optOutId } }
                     }
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(200, _optOutsResponse.SerialiseToXml(), "application/xml"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _pageNumber = 1;
            _pageSize = 15;
            _result = optOutClient.GetByFromAddress(_phoneNumber, _pageNumber, _pageSize);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("GET"));
            Assert.That(_request.Url, Is.EqualTo(string.Format("/v1.0/optouts?from={0}&startIndex={1}&count={2}", _phoneNumber, (--_pageNumber) * _pageSize, _pageSize)));
        }

        [Test]
        public void ThenTheRequestHasTheExpectedHeaders()
        {
            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/xml; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build)));
        }

        [Test]
        public void ThenTheExpectedResultIsReturned()
        {
            Assert.That(_result.OptOuts.First().Id, Is.EqualTo(_optOutsResponse.OptOuts.First().Id));
            Assert.That(_result.OptOuts.First().AccountReference, Is.EqualTo(_optOutsResponse.OptOuts.First().AccountReference));
            Assert.That(_result.OptOuts.First().ReceivedAt, Is.EqualTo(_optOutsResponse.OptOuts.First().ReceivedAt));
            Assert.That(_result.OptOuts.First().FromAddress.PhoneNumber, Is.EqualTo(_optOutsResponse.OptOuts.First().FromAddress.PhoneNumber));
            Assert.That(_result.OptOuts.First().Links.First(o => o.Rel == "self").Href, Is.EqualTo(_optOutsResponse.OptOuts.First().Links.First().Href));
        }
    }

    [TestFixture]
    public class OptOutsServiceGetAllWithAccountReferenceTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private SubscriptionCollection _result;
        private OptOutsResponse _optOutsResponse;
        private string _phoneNumber;
        private string _accountReference;
        private int _pageNumber;
        private int _pageSize;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            var optOutId = Guid.NewGuid();
            _phoneNumber = "44123456789";
            _accountReference = "EX0123456";
            _optOutsResponse = new OptOutsResponse
            {
                OptOuts = new[]
                {
                    new OptOutXmlResponse
                    {
                        Id = optOutId,
                        ReceivedAt = DateTime.UtcNow,
                        AccountReference = _accountReference,
                        FromAddress = new PhoneNumberResponse { PhoneNumber = _phoneNumber },
                        Links = new [] { new Link{ Rel = "self", Href = "https://api.esendex.com/v1.0/" + optOutId } }
                     }
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(200, _optOutsResponse.SerialiseToXml(), "application/xml"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _pageNumber = 1;
            _pageSize = 15;
            _result = optOutClient.GetByAccountReference(_accountReference, _pageNumber, _pageSize);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("GET"));
            Assert.That(_request.Url, Is.EqualTo(string.Format("/v1.0/optouts?accountreference={0}&startIndex={1}&count={2}", _accountReference, (--_pageNumber)*_pageSize, _pageSize)));
        }

        [Test]
        public void ThenTheRequestHasTheExpectedHeaders()
        {
            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/xml; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build)));
        }

        [Test]
        public void ThenTheExpectedResultIsReturned()
        {
            Assert.That(_result.OptOuts.First().Id, Is.EqualTo(_optOutsResponse.OptOuts.First().Id));
            Assert.That(_result.OptOuts.First().AccountReference, Is.EqualTo(_optOutsResponse.OptOuts.First().AccountReference));
            Assert.That(_result.OptOuts.First().ReceivedAt, Is.EqualTo(_optOutsResponse.OptOuts.First().ReceivedAt));
            Assert.That(_result.OptOuts.First().FromAddress.PhoneNumber, Is.EqualTo(_optOutsResponse.OptOuts.First().FromAddress.PhoneNumber));
            Assert.That(_result.OptOuts.First().Links.First(o => o.Rel == "self").Href, Is.EqualTo(_optOutsResponse.OptOuts.First().Links.First().Href));
        }
    }
}
