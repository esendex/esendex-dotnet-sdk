using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents a summary of a resource
    /// </summary>
    [Serializable]
    public class ResourceLink
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Uri
        /// </summary>
        [XmlAttribute("uri")]
        public string Uri { get; set; }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as ResourceLink;

            if (other == null) return false;

            if (Id != other.Id) return false;
            if (Uri != other.Uri) return false;

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