using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents a paged collection of objects which all paged collection are derived.
    /// </summary>
    /// <typeparam name="T">The type of objects.</typeparam>
    public abstract class PagedCollection<T> : IPagedCollection<T>
        where T : class
    {
        /// <summary>
        /// <![CDATA[Initialises a new instance of the com.esendex.sdk.core.PagedCollection<T>]]>
        /// </summary>
        public PagedCollection()
        {
            Items = new List<T>();
        }

        protected List<T> Items { get; set; }

        private int startIndex;

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        [XmlAttribute("startindex")]
        [JsonProperty("startindex")]
        public int PageNumber
        {
            get
            {
                // Convert the zero based collection index to a real page number.
                if (PageSize == 0 && TotalItems == 0)
                {
                    return 1;
                }
                return (startIndex/PageSize) + 1;
            }
            set { startIndex = value; }
        }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        [XmlAttribute("count")]
        [JsonProperty("count")]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total number of items in the paged resource.
        /// </summary>
        [XmlAttribute("totalcount")]
        [JsonProperty("totalcount")]
        public int TotalItems { get; set; }

        /// <summary>
        /// Determines whether the specified System.Object are considered equal.
        /// </summary>
        /// <param name="obj">The System.Object to compare with the current System.Object</param>
        /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as PagedCollection<T>;

            if (other == null) return false;

            if (PageNumber != other.PageNumber) return false;
            if (PageSize != other.PageSize) return false;
            if (TotalItems != other.TotalItems) return false;

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