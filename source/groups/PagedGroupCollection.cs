using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using com.esendex.sdk.core;

namespace com.esendex.sdk.groups
{
    /// <summary>
    /// Represents a paged collection of contacts.
    /// </summary>    
    [Serializable]
    [XmlRoot("contactgroups", Namespace = Constants.API_NAMESPACE)]
    public class PagedGroupCollection : PagedCollection<Group>
    {
        public PagedGroupCollection()
        {
            Items = new List<Group>();
        }

        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<com.esendex.sdk.groups.Group> instance that contains the groups.]]>
        /// </summary>
        [XmlElement("contactgroup")]
        public List<Group> Groups
        {
            get { return Items; }
            set { Items = value; }
        }
    }
}