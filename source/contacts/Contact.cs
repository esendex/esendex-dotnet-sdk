using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace com.esendex.sdk.contacts
{
    /// <summary>
    /// Represents a contact.
    /// </summary>
    [Serializable]
    [XmlRoot("contact", Namespace = Constants.API_NAMESPACE)]
    public class Contact : ModelBase
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.Contact
        /// </summary>
        /// <param name="quickName">A System.String instance that contains the quick name.</param>
        /// <param name="phoneNumber">A System.String instance that contains the phone number.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Contact(string quickName, string phoneNumber)
            : this()
        {
            if (string.IsNullOrEmpty(quickName)) throw new ArgumentNullException("quickName");
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException("phoneNumber");

            QuickName = quickName;
            PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.Contact
        /// </summary>
        public Contact()
        {
            Groups = new List<ContactGroupSummary>();
        }

        /// <summary>
        /// Gets or sets the Concurrency Id.
        /// </summary>
        [XmlElement("concurrencyid")]
        public Guid ConcurrencyId { get; set; }
        public bool ShouldSerializeConcurrencyId() { return ConcurrencyId != Guid.Empty; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [XmlElement("firstname")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [XmlElement("lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the quick name.
        /// </summary>
        [XmlElement("quickname")]
        public string QuickName { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        [XmlElement("mobilenumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the contact type.
        /// </summary>
        [XmlElement("type")]
        public ContactType Type { get; set; }

        /// <summary>
        /// Gets or sets the contact group summary.
        /// </summary>
        [XmlArray("groups")]
        [XmlArrayItem("groupsummary")]
        public List<ContactGroupSummary> Groups { get; set; }
        public bool ShouldSerializeGroups() { return Groups.Count > 0; }
        
        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Contact other = obj as Contact;

            if (other == null) return false;

            if (ConcurrencyId != other.ConcurrencyId) return false;
            if (FirstName !=  other.FirstName) return false;
            if (LastName !=  other.LastName) return false;
            if (QuickName !=  other.QuickName) return false;
            if (PhoneNumber != other.PhoneNumber) return false;

            for (int i = 0; i < this.Groups.Count; i++)
            {
                if (Groups[i] != other.Groups[i]) return false;
            }

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