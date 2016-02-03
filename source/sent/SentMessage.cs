using System;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.sent
{
    /// <summary>
    /// Represents a sent message.
    /// </summary>
    [Serializable]
    [XmlRoot("messageheader", Namespace = Constants.API_NAMESPACE)]
    public class SentMessage : Message
    {
        /// <summary>
        /// Gets or sets the System.DateTime at which the message was submitted at.
        /// </summary>
        [XmlElement("submittedat")]
        public DateTime SubmittedAt { get; set; }

        /// <summary>
        /// Gets or sets the System.DateTime at which the message was sent at.
        /// </summary>
        [XmlElement("sentat")]
        public DateTime? SentAt { get; set; }

        public bool ShouldSerializeSentAt()
        {
            return SentAt.HasValue;
        }

        /// <summary>
        /// Gets or sets the System.DateTime at which the message was delivered at.
        /// </summary>
        [XmlElement("deliveredat")]
        public DateTime? DeliveredAt { get; set; }

        public bool ShouldSerializeDeliveredAt()
        {
            return DeliveredAt.HasValue;
        }

        /// <summary>
        /// Gets or set the System.DateTime at which the status last changed.
        /// </summary>
        [XmlElement("laststatusat")]
        public DateTime LastStatusAt { get; set; }

        /// <summary>
        /// Gets or sets a System.String that contains the username of the sender.
        /// </summary>
        [XmlElement("username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the FailureReason which contains failure reason information of the message.
        /// </summary>
        [XmlElement("failurereason")]
        public FailureReason FailureReason { get; set; }

        internal SentMessage()
        {
        }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as SentMessage;

            if (other == null) return false;

            if (SubmittedAt != other.SubmittedAt) return false;
            if (SentAt != other.SentAt) return false;
            if (DeliveredAt != other.DeliveredAt) return false;

            return base.Equals(obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current System.Object</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}