using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace com.esendex.sdk.accounts
{
    /// <summary>
    /// Represents a account.
    /// </summary>
    [Serializable]
    [XmlRoot("account", Namespace = Constants.API_NAMESPACE)]
    public class Account : ModelBase
    {
        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        [XmlElement("reference")]
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [XmlElement("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [XmlElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        [XmlElement("type")]
        public AccountType Type { get; set; }

        /// <summary>
        /// Gets or sets the messages remaining.
        /// </summary>
        [XmlElement("messagesremaining")]
        public int MessagesRemaining { get; set; }

        /// <summary>
        /// Gets or sets the expires on.
        /// </summary>
        [XmlElement("expireson")]
        public DateTime ExpiresOn { get; set; }
        
        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            Account other = obj as Account;

            if (other == null) return false;

            if (Reference != other.Reference) return false;
            if (Label !=  other.Label) return false;
            if (Address !=  other.Address) return false;
            if (Type !=  other.Type) return false;
            if (MessagesRemaining != other.MessagesRemaining) return false;

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