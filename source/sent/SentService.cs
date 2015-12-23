using System;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.sent
{
    /// <summary>
    /// A service to retrieve sent messages.
    /// </summary>
    public class SentService : ServiceBase, ISentService
    {
        /// <summary>
        /// Initialises a new instance of the SentService
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        public SentService(string username, string password)
            : this(new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.sent.SentService
        /// </summary>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance that contains access credentials.</param>
        public SentService(EsendexCredentials credentials)
            : base(credentials)
        {
        }

        internal SentService(IRestClient restClient, ISerialiser serialiser)
            : base(restClient, serialiser)
        {
        }

        /// <summary>
        /// Gets a sent message for a specific message Id.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a sent message.</param>
        /// <returns>A com.esendex.sdk.sent.SentMessage instance that contains the sent message.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public SentMessage GetMessage(Guid id)
        {
            RestResource resource = new MessageHeadersResource(id);

            return MakeRequest<SentMessage>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.sent.SentMessageCollection instance containing sent messages.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of messages in the page.</param>
        /// <returns>A com.esendex.sdk.sent.SentMessageCollection instance that contains the sent messages.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public SentMessageCollection GetMessages(int pageNumber, int pageSize)
        {
            RestResource resource = new MessageHeadersResource(pageNumber, pageSize);

            return MakeRequest<SentMessageCollection>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.sent.SentMessageCollection instance containing sent messages.
        /// </summary>
        /// <param name="accountReference">A System.String instance that contains the account reference.</param>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of messages in the page.</param>
        /// <returns>A com.esendex.sdk.sent.SentMessageCollection instance that contains the sent messages.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public SentMessageCollection GetMessages(string accountReference, int pageNumber, int pageSize)
        {
            RestResource resource = new MessageHeadersResource(accountReference, pageNumber, pageSize);

            return MakeRequest<SentMessageCollection>(HttpMethod.GET, resource);
        }
    }
}