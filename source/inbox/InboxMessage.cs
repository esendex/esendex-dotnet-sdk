using System;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.inbox
{
    /// <summary>
    /// Represents an inbox message.
    /// </summary>
    [Serializable]
    [XmlRoot("messageheader", Namespace = Constants.API_NAMESPACE)]
    public class InboxMessage : Message
    {
        /// <summary>
        /// Gets or set the username of the user that read the message.
        /// </summary>
        [XmlElement("readby")]
        public string ReadBy { get; set; }

        /// <summary>
        /// Gets or sets the System.DateTime at which the message was read at.
        /// </summary>
        [XmlElement("readat")]
        public DateTime? ReadAt { get; set; }

        public bool ShouldSerializeReadAt()
        {
            return ReadAt.HasValue;
        }

        /// <summary>
        /// Gets or sets the System.DateTime at which the message was received at.
        /// </summary>
        [XmlElement("receivedat")]
        public DateTime? ReceivedAt { get; set; }

        public bool ShouldSerializeReceivedAt()
        {
            return ReceivedAt.HasValue;
        }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as InboxMessage;

            if (other == null) return false;

            if (ReadAt != other.ReadAt) return false;
            if (ReceivedAt != other.ReceivedAt) return false;
            if (ReadBy != other.ReadBy) return false;

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