using System;
using System.Net;
using NUnit.Framework;

namespace com.esendex.sdk.test
{
    [TestFixture]
    public class EsendexCredentialsTests
    {
        [Test]
        public void DefaultDIConstructor_UsernameAndPasswordAndProxy()
        {
            //  Arrange
            var username = "username";
            var password = "password";
            var proxy = new WebProxy();

            //  Act            
            var credentials = new EsendexCredentials(username, password, proxy);

            //  Assert
            Assert.AreEqual(username, credentials.Username);
            Assert.AreEqual(password, credentials.Password);
            Assert.AreEqual(proxy, credentials.WebProxy);
        }

        [Test]
        public void DefaultDIConstructor_ShouldAcceptDefaultWebProxy()
        {
            //  Arrange
            var username = "username";
            var password = "password";
            var proxy = WebRequest.DefaultWebProxy;

            //  Act
            var credentials = new EsendexCredentials(username, password, proxy);

            //  Assert
            Assert.AreEqual(username, credentials.Username);
            Assert.AreEqual(password, credentials.Password);
            Assert.AreEqual(proxy, credentials.WebProxy);
        }

        [Test]
        public void DefaultDIConstructor_WithSessionIdAndProxy()
        {
            //  Arrange
            var sessionId = Guid.NewGuid();
            var proxy = new WebProxy();

            //  Act            
            var credentials = new EsendexCredentials(sessionId, proxy);

            //  Assert
            Assert.AreEqual(proxy, credentials.WebProxy);
            Assert.AreEqual(sessionId, credentials.SessionId);
        }

        [Test]
        public void DefaultDIConstructor_WithSessionId()
        {
            //  Arrange
            var sessionId = Guid.NewGuid();

            //  Act            
            var credentials = new EsendexCredentials(sessionId);

            //  Assert
            Assert.IsFalse(credentials.UseProxy);
            Assert.AreEqual(sessionId, credentials.SessionId);
        }

        [Test]
        public void DefaultDIConstructor_WithNullProxy()
        {
            // Arrange
            WebProxy proxy = null;

            // Act
            try
            {
                var credentails = new EsendexCredentials("username", "password", proxy);
                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("proxy", ex.ParamName);
            }
        }

        [Test]
        public void DefaultDIConstructor_WithEmptyUsernameAndPassword()
        {
            // Arrange
            string username = null;
            var password = string.Empty;

            // Act
            try
            {
                var credentails = new EsendexCredentials(username, password);
                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("username", ex.ParamName);
            }
        }
    }
}