using System;
using System.Text;

namespace com.esendex.sdk.authenticators
{
    public class BasicAuthenticator : IAuthenticator
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public BasicAuthenticator(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Value()
        {
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(Username + ":" + Password));
        }
    }
}