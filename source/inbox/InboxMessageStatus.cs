using System;

namespace com.esendex.sdk.inbox
{
    /// <summary>
    /// Contains the values of an inbox message status.
    /// </summary>
    [Serializable]
    public enum InboxMessageStatus
    {
        /// <summary>
        /// Message marked as read.
        /// </summary>
        Read,

        /// <summary>
        /// Message marked as unread.
        /// </summary>
        Unread
    }
}