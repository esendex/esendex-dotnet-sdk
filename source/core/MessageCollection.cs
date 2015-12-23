using System.Collections.Generic;
using System.Xml.Serialization;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents a base message collection from which message collections are derived.
    /// </summary>
    /// <typeparam name="TMessage">The type of message.</typeparam>
    [XmlRoot("messageheaders", Namespace = Constants.API_NAMESPACE)]
    public abstract class MessageCollection<TMessage> : PagedCollection<TMessage> where TMessage : Message
    {
        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<TMessage> instance that contains the messages.]]>
        /// </summary>
        [XmlElement("messageheader")]
        public List<TMessage> Messages
        {
            get { return Items; }
            set { Items = value; }
        }
    }
}