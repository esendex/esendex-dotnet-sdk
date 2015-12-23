using System;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.inbox
{
    /// <summary>
    /// A service to retrieve, mark status and delete inbox messages.
    /// </summary>
    public class InboxService : ServiceBase, IInboxService
    {
        /// <summary>
        /// Initialises a new instance of the InboxService
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        public InboxService(string username, string password)
            : this(new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Initialises a new instance of the InboxService
        /// </summary>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance that contains access credentials.</param>
        public InboxService(EsendexCredentials credentials)
            : base(credentials)
        {
        }

        internal InboxService(IRestClient restClient, ISerialiser serialiser)
            : base(restClient, serialiser)
        {
        }

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessage instance containing an inbox message.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessage instance containing an inbox message.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public InboxMessage GetMessage(Guid id)
        {
            RestResource resource = new MessageHeadersResource(id);

            return MakeRequest<InboxMessage>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public InboxMessageCollection GetMessages()
        {
            RestResource resource = new InboxMessagesResource();

            return MakeRequest<InboxMessageCollection>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public InboxMessageCollection GetMessages(int pageNumber, int pageSize)
        {
            RestResource resource = new InboxMessagesResource(pageNumber, pageSize);

            return MakeRequest<InboxMessageCollection>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <param name="accountReference">A System.String instance that contains the account reference.</param>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public InboxMessageCollection GetMessages(string accountReference, int pageNumber, int pageSize)
        {
            RestResource resource = new InboxMessagesResource(accountReference, pageNumber, pageSize);

            return MakeRequest<InboxMessageCollection>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <param name="accountReference">A System.String instance that contains the account reference.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public InboxMessageCollection GetMessages(string accountReference)
        {
            RestResource resource = new InboxMessagesResource(accountReference);

            return MakeRequest<InboxMessageCollection>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Returns true if the message was successfully marked as read; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>true, if the message was successfully marked as read; otherwise, false.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public bool MarkMessageAsRead(Guid id)
        {
            RestResource resource = new InboxMessagesResource(id, InboxMessageStatus.Read);

            var response = MakeRequest(HttpMethod.PUT, resource);

            return (response != null);
        }

        /// <summary>
        /// Returns true if the message was successfully marked as unread; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>true, if the message was successfully marked as read; otherwise, false.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public bool MarkMessageAsUnread(Guid id)
        {
            RestResource resource = new InboxMessagesResource(id, InboxMessageStatus.Unread);

            var response = MakeRequest(HttpMethod.PUT, resource);

            return (response != null);
        }

        /// <summary>
        /// Returns true if the message was successfully deleted; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>true, if the message was successfully marked as read; otherwise, false.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public bool DeleteMessage(Guid id)
        {
            RestResource resource = new InboxMessagesResource(id);

            var response = MakeRequest(HttpMethod.DELETE, resource);

            return (response != null);
        }
    }
}