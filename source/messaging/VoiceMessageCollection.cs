using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Represents a collection of Voice messages
    /// </summary>
    [Serializable]
    [XmlRoot("messages", Namespace = Constants.API_NAMESPACE)]
    public class VoiceMessageCollection : MessageCollection<VoiceMessage>
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.VoiceMessageCollection
        /// </summary>
        public VoiceMessageCollection()
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.VoiceMessageCollection
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.VoiceMessage instance that contains the Voice message.</param>
        public VoiceMessageCollection(VoiceMessage message)
            : base(message)
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.VoiceMessageCollection
        /// </summary>
        /// <param name="messages"><![CDATA[A System.Collections.Generic.IEnumerable<com.esendex.sdk.messaging.VoiceMessage> instance that contains the Voice messages.]]></param>
        /// <param name="accountReference">A System.String instance that contain the Esendex Account Reference</param>
        public VoiceMessageCollection(IEnumerable<VoiceMessage> messages, string accountReference)
            : base(messages, accountReference)
        {
            SetDefaultValues();
        }

        /// <summary>
        /// Gets or sets the 
        /// </summary>
        [XmlElement("lang")]
        public VoiceMessageLanguage Language { get; set; }

        /// <summary>
        /// Gets of set the number of retries
        /// </summary>
        [XmlElement("retries")]
        public int Retries { get; set; }

        private void SetDefaultValues()
        {
            Retries = 1;
            Language = VoiceMessageLanguage.en_GB;
        }
    }
}