using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Represents a summary of the messages sent
    /// </summary>
    [Serializable]
    [XmlRoot("messageheaders", Namespace = Constants.API_NAMESPACE)]
    public class MessagingResult
    {
        /// <summary>
        /// Gets or sets the Batch Id
        /// </summary>
        [XmlAttribute("batchid")]
        public Guid BatchId { get; set; }

        /// <summary>
        /// Gets or sets the Message Ids
        /// </summary>
        [XmlElement("messageheader")]
        public List<ResourceLink> MessageIds { get; set; }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.messaging.MessagingResult
        /// </summary>
        public MessagingResult()
        {
            MessageIds = new List<ResourceLink>();
        }
    }
}