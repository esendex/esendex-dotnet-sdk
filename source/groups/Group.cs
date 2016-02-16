using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.groups
{
    /// <summary>
    /// Represents a contact.
    /// </summary>
    [Serializable]
    [XmlRoot("contactgroup", Namespace = Constants.API_NAMESPACE)]
    public class Group
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.groups.group
        /// </summary>
        /// <param name="name">A System.String instance that contains the quick name.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public Group(string accountReference, string name)
            : this()
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");

            AccountReference = accountReference;
            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.groups.group
        /// </summary>
        public Group()
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
        /// Gets or sets the name.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

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
            return Equals((Group) obj);
        }

        protected bool Equals(Group other)
        {
            return Id.Equals(other.Id)
                   && string.Equals(Name, other.Name)
                   && string.Equals(AccountReference, other.AccountReference);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (AccountReference != null ? AccountReference.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}