using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents a summary of contact.
    /// </summary>
    public class MessageContact
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        public bool ShouldSerializeID()
        {
            return (Id != Guid.Empty);
        }

        /// <summary>
        /// Gets or sets the Uri.
        /// </summary>
        [XmlAttribute("uri")]
        public string Uri { get; set; }

        public bool ShouldSerializeUri()
        {
            return (!string.IsNullOrEmpty(Uri));
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [XmlElement("displayname")]
        public string DisplayName { get; set; }

        public bool ShouldSerializeDisplayName()
        {
            return (!string.IsNullOrEmpty(DisplayName));
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [XmlElement("phonenumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.core.MessageHeaderContact
        /// </summary>
        public MessageContact()
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.core.MessageHeaderContact
        /// </summary>
        /// <param name="phoneNumber">A System.String that contains the phone number.</param>
        public MessageContact(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as MessageContact;

            if (other == null) return false;

            if (Id != other.Id) return false;
            if (Uri != other.Uri) return false;
            if (DisplayName != other.DisplayName) return false;
            if (PhoneNumber != other.PhoneNumber) return false;

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