using System;

namespace com.esendex.sdk.sent
{
    /// <summary>
    /// Defines methods to retrieve sent messages.
    /// </summary>
    public interface ISentService
    {
        /// <summary>
        /// Gets a sent item for a specific message Id.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a sent message.</param>
        /// <returns>A com.esendex.sdk.sent.SentMessage instance that contains the sent message.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        SentMessage GetMessage(Guid id);

        /// <summary>
        /// Gets a com.esendex.sdk.sent.SentMessageCollection instance containing sent messages.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.sent.SentMessageCollection instance that contains the sent messages.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        SentMessageCollection GetMessages(int pageNumber, int pageSize);

        /// <summary>
        /// Gets a com.esendex.sdk.sent.SentMessageCollection instance containing sent messages.
        /// </summary>
        /// <param name="accountReference">A System.String instance that contains the account reference.</param>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.sent.SentMessageCollection instance that contains the sent messages.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        SentMessageCollection GetMessages(string accountReference, int pageNumber, int pageSize);
    }
}