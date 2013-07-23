using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.contacts
{
    /// <summary>
    /// Represents a summary of groups associated with a contact.
    /// </summary>
    [Serializable]
    [XmlRoot("groupsummary", Namespace = Constants.API_NAMESPACE)]
    public class ContactGroupSummary : ModelBase
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            ContactGroupSummary other = obj as ContactGroupSummary;

            if (other == null) return false;

            if (Name != other.Name) return false;

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