using System;

namespace com.esendex.sdk.inbox
{
    /// <summary>
    /// Defines methods to retrieve inbox messages.
    /// </summary>
    public interface IInboxService
    {
        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessage instance containing an inbox message.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessage instance containing an inbox message.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        InboxMessage GetMessage(Guid id);

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        InboxMessageCollection GetMessages();

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        InboxMessageCollection GetMessages(int pageNumber, int pageSize);

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <param name="accountReference">A System.String instance that contains the account reference.</param>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        InboxMessageCollection GetMessages(string accountReference, int pageNumber, int pageSize);

        /// <summary>
        /// Gets a com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.
        /// </summary>
        /// <param name="accountReference">A System.String instance that contains the account reference.</param>
        /// <returns>A com.esendex.sdk.inbox.InboxMessageCollection instance containing inbox messages.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        InboxMessageCollection GetMessages(string accountReference);

        /// <summary>
        /// Returns true if the message was successfully marked as read; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>true, if the message was successfully marked as read; otherwise, false.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        bool MarkMessageAsRead(Guid id);

        /// <summary>
        /// Returns true if the message was successfully marked as unread; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>true, if the message was successfully marked as read; otherwise, false.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        bool MarkMessageAsUnread(Guid id);

        /// <summary>
        /// Returns true if the message was successfully deleted; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of an inbox message.</param>
        /// <returns>true, if the message was successfully marked as read; otherwise, false.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        bool DeleteMessage(Guid id);
    }
}