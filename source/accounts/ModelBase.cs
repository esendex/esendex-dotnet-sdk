using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.accounts
{
    /// <summary>
    /// Represents a base model from which models in the com.esendex.sdk.contacts are derived.
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// Gets or sets a the Id.
        /// </summary>
        [XmlAttribute("id")]
        public Guid Id { get; set; }
        public bool ShouldSerializeId() { return Id != Guid.Empty; }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [XmlAttribute("uri")]
        public string Uri { get; set; }
        public bool ShouldSerializeUri() { return !string.IsNullOrEmpty(Uri); }


        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ModelBase other = obj as ModelBase;

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