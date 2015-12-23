using System;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.session
{
    /// <summary>
    /// A service to manage session authentication.
    /// </summary>
    public class SessionService : ServiceBase, ISessionService
    {
        /// <summary>
        /// Initialises a new instance of the SessionService
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        public SessionService(string username, string password)
            : this(new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.session.SessionService
        /// </summary>
        public SessionService(EsendexCredentials credentials)
            : base(credentials)
        {
        }

        internal SessionService(IRestClient restClient, ISerialiser serialiser)
            : base(restClient, serialiser)
        {
        }

        /// <summary>
        /// Creates a System.Guid instance that contains the session id.
        /// </summary>
        /// <returns>A System.Guid instance that contains the session id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public Guid CreateSession()
        {
            RestResource resource = new SessionResource();

            var session = MakeRequest<EsendexSession>(HttpMethod.POST, resource);

            return session.Id;
        }
    }
}