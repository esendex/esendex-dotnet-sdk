using System;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Represents a collection of Voice message
    /// </summary>
    [Serializable]
    [XmlRoot("message")]
    public class VoiceMessage : Message
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.VoiceMessage
        /// </summary>
        public VoiceMessage()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.VoiceMessage
        /// </summary>
        /// <param name="recipients">A System.String instance that contains comma delimited string of recipients.</param>
        /// <param name="body">A System.String instance that contains the message body text.</param>
        /// <param name="accountReference">A System.String instance that contains the Esendex Account Reference.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public VoiceMessage(string recipients, string body, string accountReference)
            : base(recipients, body, accountReference)
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Gets or sets the message language
        /// </summary>
        [XmlElement("lang")]
        public VoiceMessageLanguage Language { get; set; }

        /// <summary>
        /// Gets or sets the number of message retries
        /// </summary>
        [XmlElement("retries")]
        public int Retries { get; set; }

        /// <summary>
        /// Gets the message type
        /// </summary>
        [XmlElement("type")]
        public override MessageType Type
        {
            get { return MessageType.Voice; }
            set { }
        }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as VoiceMessage;

            if (other == null) return false;

            if (Language != other.Language) return false;
            if (Retries != other.Retries) return false;

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

        private void SetDefaultValues()
        {
            Retries = 1;
            Language = VoiceMessageLanguage.en_GB;
        }
    }
}