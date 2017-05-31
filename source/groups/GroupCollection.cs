using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace com.esendex.sdk.groups
{
    /// <summary>
    /// Represents a collecion of groups.
    /// </summary>
    [Serializable]
    [XmlRoot("contactgroups", Namespace = Constants.API_NAMESPACE)]
    public class GroupCollection
    {
        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.groups.GroupCollection
        /// </summary>
        public GroupCollection()
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.groups.GroupCollection
        /// </summary>
        /// <param name="group">A com.esendex.sdk.groups.Group instance that contains a contact.</param>
        public GroupCollection(Group group)
        {
            if (group == null) throw new ArgumentNullException("contactgroup");

            Items.Add(group);
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.groups.GroupCollection
        /// </summary>
        /// <param name="groups"><![CDATA[A System.Collections.Generic.IEnumerable<com.esendex.sdk.groups.Group> instance that contains the groups.]]></param>
        public GroupCollection(IEnumerable<Group> groups)
        {
            if (groups == null) throw new ArgumentNullException("contactgroups");

            Items.AddRange(groups);
        }

        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<com.esendex.sdk.groups.Group> instance that contains the groups.]]>
        /// </summary>
        [XmlElement("contact")] public List<Group> Items = new List<Group>();

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as GroupCollection;

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