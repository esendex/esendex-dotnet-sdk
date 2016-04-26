using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models;
using com.esendex.sdk.test.mockapi;
using Newtonsoft.Json;
using NUnit.Framework;
using com.esendex.sdk.test.models;

namespace com.esendex.sdk.test.optouts
{
    [TestFixture]
    public class OptOutsServiceGetByIdTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private OptOut _result;
        private OptOutXmlResponse _optOut;

        [TestFixtureSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";

            var optOutId = Guid.NewGuid();
            _optOut = new OptOutXmlResponse
            {
                Id = optOutId,
                ReceivedAt = DateTime.UtcNow,
                AccountReference = "EX0123456",
                FromAddress = new PhoneNumberResponse
                {
                    PhoneNumber = "44123456789"
                },
                Links = new[] {new models.Link{Rel = "self", Href = "TheLinkForThisOptOut"}}
            };

            MockApi.SetEndpoint(new MockEndpoint(200, _optOut.SerialiseToXml(), "application/xml"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _result = optOutClient.GetById(_optOut.Id);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("GET"));
            Assert.That(_request.Url, Is.EqualTo(string.Format("/v1.0/optouts/{0}", _optOut.Id)));
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
            Assert.That(_result.Id, Is.EqualTo(_optOut.Id));
            Assert.That(_result.AccountReference, Is.EqualTo(_optOut.AccountReference));
            Assert.That(_result.ReceivedAt, Is.EqualTo(_optOut.ReceivedAt));
            Assert.That(_result.FromAddress.PhoneNumber, Is.EqualTo(_optOut.FromAddress.PhoneNumber));
            Assert.That(_result.Links.First(o => o.Rel == "self").Href, Is.EqualTo(_optOut.Links.First().Href));
        }
    }
}
