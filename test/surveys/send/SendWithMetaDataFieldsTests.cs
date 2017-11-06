using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.esendex.sdk.surveys;
using com.esendex.sdk.test.mockapi;
using com.esendex.sdk.test.models.requests.surveys;
using Newtonsoft.Json;
using NUnit.Framework;

namespace com.esendex.sdk.test.surveys.send
{
    [TestFixture]
    public class SendWithMetadataFieldsTests
    {
        private readonly Version _version = Assembly.GetAssembly(typeof(SurveySendService)).GetName().Version;
        private Guid _surveyId;
        private string _recipient;
        private Dictionary<string, string> _metaDataFields;
        private mockapi.Request _request;
        private string _expectedUrl;
        private string _expectedUserAgent;

        [OneTimeSetUp]
        public void Given()
        {
            const string username = "user@example.com";
            const string password = "heythiscantbeguessed";
            _recipient = "44123456789";
            _surveyId = Guid.NewGuid();

            _expectedUrl = string.Format("/v1.0/surveys/{0}/send", _surveyId);
            _expectedUserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);

            MockApi.SetEndpoint(new MockEndpoint(200, contentType: "text/plain"));

            var surveysClient = new SurveySendService(MockApi.Url, new EsendexCredentials(username, password));
            _metaDataFields = new Dictionary<string, string> { {"Field", "Value"}};

            surveysClient.Send(_surveyId, _recipient , null, _metaDataFields);
            _request = MockApi.LastRequest;
        }

        [Test]
        public void ThenTheExpectedRequestIsMade()
        {
            Assert.That(_request.Method, Is.EqualTo("POST"));
            Assert.That(_request.Url, Is.EqualTo(_expectedUrl));
        }

        [Test]
        public void ThenTheRequestHasTheExpectedBody()
        {
            var body = JsonConvert.DeserializeObject<SurveyAddRecipientsRequest>(_request.Body);
            var recipient = body.Recipients.First();

            Assert.That(body.Recipients.Count, Is.EqualTo(1));
            Assert.That(recipient.PhoneNumber, Is.EqualTo(_recipient));
            CollectionAssert.AreEquivalent(_metaDataFields, recipient.MetaData);
        }

        [Test]
        public void ThenTheRequestHasTheExpectedHeaders()
        {
            Assert.That(_request.Headers["Accept"], Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(_request.Headers["Authorization"], Is.EqualTo("Basic dXNlckBleGFtcGxlLmNvbTpoZXl0aGlzY2FudGJlZ3Vlc3NlZA=="));
            Assert.That(_request.Headers["Content-Type"], Is.EqualTo("application/json; charset=utf-8"));
            Assert.That(_request.Headers["User-Agent"], Is.EqualTo(_expectedUserAgent));
        }
    }
}