using System.Xml.Serialization;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents the body text of a message.
    /// </summary>
    [XmlRoot("messagebody", Namespace = Constants.API_NAMESPACE)]
    public class MessageBody : ResourceLink
    {
        /// <summary>
        /// A System.String instance containing the text.
        /// </summary>
        [XmlElement("bodytext")]
        public string BodyText { get; set; }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as MessageBody;

            if (other == null) return false;
            if (BodyText != other.BodyText) return false;

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
    }
}