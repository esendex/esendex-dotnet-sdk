using System;
using com.esendex.sdk.authenticators;
using NUnit.Framework;

namespace com.esendex.sdk.test.authenticators
{
    [TestFixture]
    public class SessionAuthenticatorTests
    {
        [Test]
        public void ThenTheValueIsCorrect()
        {
            var authenticator = new SessionAuthenticator(Guid.Parse("36468135-13c6-4a1c-bdcb-2e81a4807249"));
            Assert.That(authenticator.Value(), Is.EqualTo("Basic MzY0NjgxMzUtMTNjNi00YTFjLWJkY2ItMmU4MWE0ODA3MjQ5"));
        }
    }
}