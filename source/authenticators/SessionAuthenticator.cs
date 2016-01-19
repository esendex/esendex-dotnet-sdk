using System;
using System.Text;

namespace com.esendex.sdk.authenticators
{
    public class SessionAuthenticator : IAuthenticator
    {
        private readonly Guid _sessionId;

        public SessionAuthenticator(Guid sessionId)
        {
            _sessionId = sessionId;
        }

        public string Value()
        {
            return "Basic " +  Convert.ToBase64String(Encoding.UTF8.GetBytes(_sessionId.ToString()));
        }
    }
}