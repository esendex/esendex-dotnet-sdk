using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.esendex.sdk.surveys.models
{
    /// <summary>
    /// An enum representing the delivery status of a sent question
    /// </summary>
    public enum DeliveryStatus
    {
        /// <summary>
        /// Represents that the question was successfully delivered
        /// </summary>
        Delivered,

        /// <summary>
        /// Represents that the question failed to send
        /// </summary>
        Failed,

        /// <summary>
        /// Represents that the question has been submitted to the system but 
        /// has not yet been marked as Delivered or Failed
        /// </summary>
        Submitted
    }
}
