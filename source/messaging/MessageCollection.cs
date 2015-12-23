using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Represents a base message collection from which message collections are derived.
    /// </summary>
    /// <typeparam name="TMessage">The type of message.</typeparam>
    public abstract class MessageCollection<TMessage> where TMessage : Message
    {
        /// <summary>
        /// <![CDATA[Initialises a new instance of the com.esendex.sdk.messaging.MessageCollection<TMessage>]]>
        /// </summary>
        public MessageCollection()
        {
        }

        /// <summary>
        /// <![CDATA[Initialises a new instance of the com.esendex.sdk.messaging.Message<TMessage>]]>
        /// </summary>
        /// <param name="message">A message instance derived from com.esendex.sdk.messaging.Message</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public MessageCollection(TMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");

            Items.Add(message);

            AccountReference = message.AccountReference;
        }

        /// <summary>
        /// <![CDATA[Initialises a new instance of the com.esendex.sdk.messaging.Message<TMessage>]]>
        /// </summary>
        /// <param name="messages"><![CDATA[A System.Collections.Generic.IEnumerable<TMessage> instance that contains the messages.]]></param>
        /// <param name="accountReference">A System.String instance that contains the Esendex Account Reference.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public MessageCollection(IEnumerable<TMessage> messages, string accountReference)
        {
            if (messages == null) throw new ArgumentNullException("messages");
            if (string.IsNullOrEmpty(accountReference)) throw new ArgumentNullException("accountReference");

            Items.AddRange(messages);

            AccountReference = accountReference;
        }

        /// <summary>
        /// Gets or sets the account reference
        /// </summary>
        [XmlElement("accountreference")]
        public string AccountReference { get; set; }

        /// <summary>
        /// Gets or sets the originator
        /// </summary>
        [XmlElement("from")]
        public string Originator { get; set; }

        public bool ShouldSerializeOriginator()
        {
            return !string.IsNullOrEmpty(Originator);
        }

        /// <summary>
        /// Gets or sets the message type
        /// </summary>
        [XmlElement("type")]
        public virtual MessageType? Type { get; set; }

        public bool ShouldSerializeType()
        {
            return Type.HasValue;
        }

        /// <summary>
        /// Gets or sets the validity period
        /// </summary>
        [XmlElement("validity")]
        public int? ValidityPeriod { get; set; }

        public bool ShouldSerializeValidityPeriod()
        {
            return ValidityPeriod.HasValue;
        }

        /// <summary>
        /// Gets or sets a timestamp for scheduling 
        /// </summary>
        [XmlElement("sendat")]
        public DateTime? SendAt { get; set; }

        public bool ShouldSerializeSendAt()
        {
            return SendAt.HasValue;
        }

        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<TMessage> instance that contains the messages.]]>
        /// </summary>
        [XmlElement("message")] public List<TMessage> Items = new List<TMessage>();

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as MessageCollection<TMessage>;

            if (other == null) return false;

            if (AccountReference != other.AccountReference) return false;
            if (Originator != other.Originator) return false;
            if (Type != other.Type) return false;
            if (ValidityPeriod != other.ValidityPeriod) return false;
            if (SendAt != other.SendAt) return false;

            if (Items.Count != other.Items.Count) return false;

            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i] != other.Items[i]) return false;
            }

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