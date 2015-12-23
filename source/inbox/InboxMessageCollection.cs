using System;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.inbox
{
    /// <summary>
    /// Represents a collecion of inbox messages.
    /// </summary>
    [Serializable]
    [XmlRoot("messageheaders", Namespace = Constants.API_NAMESPACE)]
    public class InboxMessageCollection : MessageCollection<InboxMessage>
    {
    }
}