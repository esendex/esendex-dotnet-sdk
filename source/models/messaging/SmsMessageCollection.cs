using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Represents a collection of SMS messages.
    /// </summary>
    [Serializable]
    [XmlRoot("messages", Namespace = Constants.API_NAMESPACE)]
    public class SmsMessageCollection : MessageCollection<SmsMessage>
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.SmsMessageCollection
        /// </summary>
        public SmsMessageCollection()
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.SmsMessageCollection
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.SmsMessage instance that contains the SMS message.</param>
        public SmsMessageCollection(SmsMessage message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.SmsMessageCollection
        /// </summary>
        /// <param name="messages"><![CDATA[A System.Collections.Generic.IEnumerable<com.esendex.sdk.messaging.SmsMessage> instance that contains the SMS messages.]]></param>
        /// <param name="accountReference">A System.String instance that contain the Esendex Account Reference.</param>
        public SmsMessageCollection(IEnumerable<SmsMessage> messages, string accountReference)
            : base(messages, accountReference)
        {
        }
    }
}