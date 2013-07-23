using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.contacts
{
    /// <summary>
    /// Represents a paged collection of contacts.
    /// </summary>    
    [Serializable]
    [XmlRoot("contacts", Namespace = Constants.API_NAMESPACE)]
    public class PagedContactCollection : PagedCollection<Contact>
    {
        public PagedContactCollection()
        {
            Items = new List<Contact>();
        }

        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<com.esendex.sdk.contacts.Contact> instance that contains the contacts.]]>
        /// </summary>
        [XmlElement("contact")]
        public List<Contact> Contacts
        {
            get { return Items; }
            set { Items = value; }
        }
    }
}