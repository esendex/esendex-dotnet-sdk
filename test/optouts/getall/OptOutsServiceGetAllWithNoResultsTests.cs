using System;
using System.Reflection;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models.response;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts.getall
{
    [TestFixture]
    public class OptOutsServiceGetAllWithNoResultsTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private OptOutCollection _result;
        private int _pageSize;
        private int _pageNumber;
        private string _expectedUrl;
        private string _expectedUserAgent;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            var data = new
            {
                StartIndex = 0,
                Count = 0,
                TotalCount = 0
            };

            _pageSize = 15;
            _pageNumber = 1;

            _expectedUrl = string.Format("/v1.0/optouts?startIndex={0}&count={1}", (_pageNumber - 1) * _pageSize, _pageSize);
            _expectedUserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

            MockApi.SetEndpoint(new MockEndpoint(200, JsonConvert.SerializeObject(data), "application/json"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _result = optOutClient.GetAll(_pageNumber, _pageSize);
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
            Assert.That(_result.PageNumber, Is.EqualTo(1));
            Assert.That(_result.PageSize, Is.EqualTo(0));
            Assert.That(_result.TotalItems, Is.EqualTo(0));

            Assert.That(_result.OptOuts.Count, Is.EqualTo(0));
        }
    }
}