using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace com.esendex.sdk.accounts
{
    /// <summary>
    /// Represents a collecion of accounts.
    /// </summary>
    [Serializable]
    [XmlRoot("contacts", Namespace = Constants.API_NAMESPACE)]
    public class AccountCollection
    {
        public AccountCollection()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        public AccountCollection(Account account)
            : base()
        {
            if (account == null) throw new ArgumentNullException("account");

            Items.Add(account);
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.accounts.AccountCollection
        /// </summary>
        /// <param name="accounts"><![CDATA[A System.Collections.Generic.IEnumerable<com.esendex.sdk.accounts.Account> instance that contains the accounts.]]></param>
        public AccountCollection(IEnumerable<Account> accounts)
        {
            if (accounts == null) throw new ArgumentNullException("accounts");

            Items.AddRange(accounts);
        }

        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<com.esendex.sdk.accounts.Account> instance that contains the accounts.]]>
        /// </summary>
        [XmlElement("account")] public List<Account> Items = new List<Account>();

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            AccountCollection other = obj as AccountCollection;

            if (other == null) return false;

            if (Items.Count != other.Items.Count) return false;

            return !Items.Where((t, i) => Items.ElementAt(i) != other.Items.ElementAt(i)).Any();
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
