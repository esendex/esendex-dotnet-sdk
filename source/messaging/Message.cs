using System;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Represents a base message from which messages are derived.
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.Message
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.Message
        /// </summary>
        /// <param name="recipients">A System.String instance that contains comma delimited string of recipients.</param>
        /// <param name="body">A System.String instance that contains the message body text.</param>
        /// <param name="accountReference">A System.String instance that contains the Esendex Account Reference.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Message(string recipients, string body, string accountReference)
        {
            if (string.IsNullOrEmpty(recipients)) throw new ArgumentNullException("recipients");
            if (string.IsNullOrEmpty(body)) throw new ArgumentNullException("body");
            if (string.IsNullOrEmpty(accountReference)) throw new ArgumentNullException("accountReference");

            AccountReference = accountReference;
            Body = body;
            Recipients = recipients;
        }

        /// <summary>
        /// Gets the account reference.
        /// </summary>
        [XmlIgnore]
        public string AccountReference { get; private set; }

        /// <summary>
        /// Gets or set the originator.
        /// </summary>
        [XmlElement("from")]
        public string Originator { get; set; }

        public bool ShouldSerializeOriginator()
        {
            return !string.IsNullOrEmpty(Originator);
        }

        /// <summary>
        /// Gets or sets the recipients.
        /// </summary>
        [XmlElement("to")]
        public string Recipients { get; set; }

        /// <summary>
        /// Gets or sets the message type.
        /// </summary>
        [XmlElement("type")]
        public abstract MessageType Type { get; set; }

        /// <summary>
        /// Gets or sets the message body text.
        /// </summary>
        [XmlElement("body")]
        public string Body { get; set; }

        /// <summary>
        /// Gets or set the validity period.
        /// </summary>
        [XmlElement("validity")]
        public int ValidityPeriod { get; set; }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Message;

            if (other == null) return false;

            if (AccountReference != other.AccountReference) return false;
            if (Originator != other.Originator) return false;
            if (Recipients != other.Recipients) return false;
            if (Type != other.Type) return false;
            if (Body != other.Body) return false;
            if (ValidityPeriod != other.ValidityPeriod) return false;

            return true;
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