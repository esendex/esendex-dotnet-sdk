using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.session
{
    /// <summary>
    /// Respresents the session identifier.
    /// </summary>
    [Serializable]
    [XmlRoot("session", Namespace = Constants.API_NAMESPACE)]
    public class EsendexSession
    {
        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        [XmlElement("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as EsendexSession;

            if (other == null) return false;

            if (Id != other.Id) return false;

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