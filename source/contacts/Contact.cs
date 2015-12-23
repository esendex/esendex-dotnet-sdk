using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.contacts
{
    /// <summary>
    /// Represents a contact.
    /// </summary>
    [Serializable]
    [XmlRoot("contact", Namespace = Constants.API_NAMESPACE)]
    public class Contact
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.Contact
        /// </summary>
        /// <param name="quickName">A System.String instance that contains the quick name.</param>
        /// <param name="phoneNumber">A System.String instance that contains the phone number.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Contact(string accountReference, string quickName, string phoneNumber)
            : this()
        {
            if (string.IsNullOrEmpty(quickName)) throw new ArgumentNullException("quickName");
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException("phoneNumber");

            AccountReference = accountReference;
            QuickName = quickName;
            PhoneNumber = phoneNumber;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.Contact
        /// </summary>
        public Contact()
        {
        }

        /// <summary>
        /// Gets or sets a the Id.
        /// </summary>
        [XmlAttribute("id")]
        public Guid Id { get; set; }

        public bool ShouldSerializeId()
        {
            return Id != Guid.Empty;
        }

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
        /// Gets or sets the phone number.
        /// </summary>
        [XmlElement("phonenumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the account reference.
        /// </summary>
        [XmlElement("accountreference")]
        public string AccountReference { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Contact) obj);
        }

        protected bool Equals(Contact other)
        {
            return Id.Equals(other.Id)
                   && string.Equals(FirstName, other.FirstName)
                   && string.Equals(LastName, other.LastName)
                   && string.Equals(QuickName, other.QuickName)
                   && string.Equals(PhoneNumber, other.PhoneNumber)
                   && string.Equals(AccountReference, other.AccountReference);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (QuickName != null ? QuickName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (PhoneNumber != null ? PhoneNumber.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (AccountReference != null ? AccountReference.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}