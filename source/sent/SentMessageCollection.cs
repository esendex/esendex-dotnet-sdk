using System;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.sent
{
    /// <summary>
    /// Represents a collecion of sent messages.
    /// </summary>
    [Serializable]
    [XmlRoot("messageheaders", Namespace = Constants.API_NAMESPACE)]
    public class SentMessageCollection : MessageCollection<SentMessage>
    {
    }
}