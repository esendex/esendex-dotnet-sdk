using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace com.esendex.sdk.contacts
{
    /// <summary>
    /// Represents a collecion of contacts.
    /// </summary>
    [Serializable]
    [XmlRoot("contacts", Namespace = Constants.API_NAMESPACE)]
    public class ContactCollection
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.ContactCollection
        /// </summary>
        public ContactCollection()
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.ContactCollection
        /// </summary>
        /// <param name="contact">A com.esendex.sdk.contacts.Contact instance that contains a contact.</param>
        public ContactCollection(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException("contact");

            Items.Add(contact);
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.contacts.ContactCollection
        /// </summary>
        /// <param name="contacts"><![CDATA[A System.Collections.Generic.IEnumerable<com.esendex.sdk.contacts.Contact> instance that contains the contacts.]]></param>
        public ContactCollection(IEnumerable<Contact> contacts)
        {
            if (contacts == null) throw new ArgumentNullException("contacts");

            Items.AddRange(contacts);
        }

        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<com.esendex.sdk.contacts.Contact> instance that contains the contacts.]]>
        /// </summary>
        [XmlElement("contact")] public List<Contact> Items = new List<Contact>();

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as ContactCollection;

            if (other == null) return false;

            if (Items.Count != other.Items.Count) return false;

            for (var i = 0; i < Items.Count; i++)
            {
                if (Items.ElementAt(i) != other.Items.ElementAt(i)) return false;
            }

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