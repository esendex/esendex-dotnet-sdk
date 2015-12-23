using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents a message.
    /// </summary>
    [XmlRoot("messageheader", Namespace = Constants.API_NAMESPACE)]
    public class Message
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Uri.
        /// </summary>
        [XmlAttribute("uri")]
        public string Uri { get; set; }

        public bool ShouldSerializeUri()
        {
            return Uri != null;
        }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        [XmlElement("type")]
        public MessageType Type { get; set; }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        [XmlElement("to")]
        public MessageContact Recipient { get; set; }

        /// <summary>
        /// Gets or set the originator.
        /// </summary>
        [XmlElement("from")]
        public MessageContact Originator { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        [XmlElement("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the message body.
        /// </summary>
        [XmlElement("body")]
        public MessageBody Body { get; set; }

        /// <summary>
        /// Gets or sets the direction of the message.
        /// </summary>
        [XmlElement("direction")]
        public MessageDirection? Direction { get; set; }

        public bool ShouldSerializeDirection()
        {
            return Direction.HasValue;
        }

        /// <summary>
        /// Gets or sets the number of parts of the message.
        /// </summary>
        [XmlElement("parts")]
        public int Parts { get; set; }

        /// <summary>
        /// Gets or sets the message status.
        /// </summary>
        [XmlElement("status")]
        public MessageStatus Status { get; set; }

        /// <summary>
        /// true if the specified com.esendex.sdk.core.Message is partial; otherwise, false.
        /// </summary>
        [XmlIgnore]
        public bool IsPartial { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        [XmlElement("index")]
        public int? Index { get; set; }

        public bool ShouldSerializeIndex()
        {
            return Index.HasValue;
        }

        /// <summary>
        /// Gets or sets the reference
        /// </summary>
        [XmlElement("reference")]
        public string AccountReference { get; set; }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.core.Message
        /// </summary>
        public Message()
        {
            Body = new MessageBody();
            Recipient = new MessageContact();
            Originator = new MessageContact();
        }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Message;

            if (other == null) return false;

            if (Id != other.Id) return false;
            if (Status != other.Status) return false;
            if (Body != other.Body) return false;
            if (Direction != other.Direction) return false;
            if (Index != other.Index) return false;
            if (Originator != other.Originator) return false;
            if (Parts != other.Parts) return false;
            if (Recipient != other.Recipient) return false;
            if (Summary != other.Summary) return false;
            if (Type != other.Type) return false;
            if (Uri != other.Uri) return false;
            if (AccountReference != other.AccountReference) return false;

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