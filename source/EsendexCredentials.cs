using System;
using System.Net;
using System.Text;

namespace com.esendex.sdk
{
    /// <summary>
    /// Represents authentication details required to access Esendex services.
    /// </summary>
    [Serializable]
    public class EsendexCredentials
    {
        internal EsendexCredentials()
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.EsendexCredentials
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public EsendexCredentials(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password");

            Username = username;
            Password = password;
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.EsendexCredentials
        /// </summary>
        /// <param name="sessionId">A System.Guid instance containing the session id.</param>
        public EsendexCredentials(Guid sessionId)
        {
            SessionId = sessionId;
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.EsendexCredentials
        /// </summary>
        /// <param name="sessionId">A System.Guid instance containing the session id.</param>
        /// <param name="proxy">A System.Net.WebProxy instance that contains proxy information required by the local network.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public EsendexCredentials(Guid sessionId, IWebProxy proxy)
            : this(sessionId)
        {
            SetProxy(proxy);
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.EsendexCredentials
        /// </summary>
        /// <param name="username">A System.String instance that contains the Esendex username.</param>
        /// <param name="password">A System.String instance that contains the Esendex password.</param>
        /// <param name="proxy">A System.Net.WebProxy instance that contains proxy information required by the local network.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public EsendexCredentials(string username, string password, IWebProxy proxy)
            : this(username, password)
        {
            SetProxy(proxy);
        }

        /// <summary>
        /// Gets the Esendex username.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the Esendex password.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Gets the proxy information.
        /// </summary>
        public IWebProxy WebProxy { get; private set; }

        /// <summary>
        /// Returns true if a System.Net.WebProxy instance was supplied to the constructor; otherwise, false.
        /// </summary>
        public bool UseProxy
        {
            get { return WebProxy != null; }
        }

        /// <summary>
        /// Returns true if a System.Guid instance was supplied to the constructor; otherwise, false.
        /// </summary>
        public bool UseSessionAuthentication
        {
            get { return SessionId.HasValue; }
        }

        /// <summary>
        /// Gets or sets a System.Guid instance containing the session id.
        /// </summary>
        public Guid? SessionId { get; set; }

        private void SetProxy(IWebProxy proxy)
        {
            if (proxy == null) throw new ArgumentNullException("proxy");

            WebProxy = proxy;
        }

        internal string EncodedValue()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(UseSessionAuthentication ? SessionId.Value.ToString() : Username + ":" + Password));
        }
    }
}