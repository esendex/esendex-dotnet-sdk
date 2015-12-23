using System;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Represents an SMS message
    /// </summary>
    [Serializable]
    [XmlRoot("message")]
    public class SmsMessage : Message
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.SmsMessage
        /// </summary>
        public SmsMessage()
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.SmsMessage
        /// </summary>
        /// <param name="recipients">A System.String instance that contains comma delimited string of recipients.</param>
        /// <param name="body">A System.String instance that contains the message body text.</param>
        /// <param name="accountReference">A System.String instance that contains the Esendex Account Reference.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public SmsMessage(string recipients, string body, string accountReference)
            : base(recipients, body, accountReference)
        {
        }

        /// <summary>
        /// Gets the message type
        /// </summary>
        [XmlElement("type")]
        public override MessageType Type
        {
            get { return MessageType.SMS; }
            set { }
        }

        /// <summary>
        /// Gets and sets the message character set
        /// </summary>
        [XmlElement("characterset")]
        public CharacterSet CharacterSet { get; set; }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as SmsMessage;

            if (other == null) return false;

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

        /// <summary>
        /// Dictates whether character set should be serialised.
        /// </summary>
        /// <returns>Whether character set should be serialised</returns>
        public bool ShouldSerializeCharacterSet()
        {
            return CharacterSet != CharacterSet.Default;
        }
    }
}