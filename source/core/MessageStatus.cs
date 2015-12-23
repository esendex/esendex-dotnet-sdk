using System;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Contains the values of a message status.
    /// </summary>
    [Serializable]
    public enum MessageStatus
    {
        /// <summary>
        /// Submitted
        /// </summary>
        Submitted,

        /// <summary>
        /// Sent
        /// </summary>
        Sent,

        /// <summary>
        /// Delivered
        /// </summary>
        Delivered,

        /// <summary>
        /// Expired
        /// </summary>
        Expired,

        /// <summary>
        /// Failed
        /// </summary>
        Failed,

        /// <summary>
        /// Failed Authorisation
        /// </summary>
        FailedAuthorisation,

        /// <summary>
        /// Acknowledged
        /// </summary>
        Acknowledged,

        /// <summary>
        /// Connecting
        /// </summary>
        Connecting,

        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled,

        /// <summary>
        /// Scheduled
        /// </summary>
        Scheduled,

        /// <summary>
        /// Partially Delivered
        /// </summary>
        PartiallyDelivered,

        /// <summary>
        /// Rejected
        /// </summary>
        Rejected
    }
}