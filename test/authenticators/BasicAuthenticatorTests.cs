using com.esendex.sdk.authenticators;
using NUnit.Framework;

namespace com.esendex.sdk.test.authenticators
{
    [TestFixture]
    public class BasicAuthenticatorTests
    {
        [Test]
        public void ThenTheValueIsCorrect()
        {
            var authenticator = new BasicAuthenticator("user", "test");
            Assert.That(authenticator.Value(), Is.EqualTo("Basic dXNlcjp0ZXN0"));
        }
    }
}
