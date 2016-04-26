using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using com.esendex.sdk.core;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models;
using com.esendex.sdk.test.mockapi;
using com.esendex.sdk.test.models;
using NUnit.Framework;

namespace com.esendex.sdk.test.optouts
{
    [TestFixture]
    public class OptOutsServiceAddTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(OptOutsService)).GetName().Version;
        private mockapi.Request _request;
        private OptOut _result;
        private OptOutCreateRootResponse _optOutsResponse;
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
            _optOutsResponse = new OptOutCreateRootResponse
            {
                OptOut = new OptOutXmlResponse
                {
                    Id = optOutId,
                    ReceivedAt = DateTime.UtcNow,
                    AccountReference = _accountReference,
                    FromAddress = new PhoneNumberResponse {PhoneNumber = _phoneNumber},
                    Links = new[] {new models.Link {Rel = "self", Href = "https://api.esendex.com/v1.0/" + optOutId}}
                }
            };

            MockApi.SetEndpoint(new MockEndpoint(200, _optOutsResponse.SerialiseToXml(), "application/xml"));

            var optOutClient = new OptOutsService(MockApi.Url, new EsendexCredentials(username, password));

            _pageNumber = 1;
            _pageSize = 15;
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
            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/xml; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build)));
        }

        [Test]
        public void ThenTheExpectedResultIsReturned()
        {
            Assert.That(_result.Id, Is.EqualTo(_optOutsResponse.OptOut.Id));
            Assert.That(_result.AccountReference, Is.EqualTo(_optOutsResponse.OptOut.AccountReference));
            Assert.That(_result.ReceivedAt, Is.EqualTo(_optOutsResponse.OptOut.ReceivedAt));
            Assert.That(_result.FromAddress.PhoneNumber, Is.EqualTo(_optOutsResponse.OptOut.FromAddress.PhoneNumber));
            Assert.That(_result.Links.First(o => o.Rel == "self").Href, Is.EqualTo(_optOutsResponse.OptOut.Links.First().Href));
        }
    }

    [XmlRoot("response", Namespace = Constants.API_NAMESPACE)]
    public class OptOutCreateRootResponse
    {
        [XmlElement("optout")]
        public OptOutXmlResponse OptOut { get; set; }

        [XmlElement("errors")]
        public List<ErrorResponse> Errors { get; set; }

        public string SerialiseToXml()
        {
            var serializer = new XmlSerializer(typeof(OptOutCreateRootResponse));
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter))
            {
                serializer.Serialize(writer, this);
                return stringWriter.ToString();
            }
        }
    }

    public class ErrorResponse
    {
        [XmlElement("code")]
        public string Code { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("link")]
        public models.Link[] Links { get; set; }
    }

}
