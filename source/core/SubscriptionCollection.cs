using System.Collections.Generic;
using System.Xml.Serialization;
using com.esendex.sdk.optouts;
using com.esendex.sdk.optouts.models;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents a collection of Opt Outs
    /// </summary>
    [XmlRoot("optouts", Namespace = Constants.API_NAMESPACE)]
    public abstract class SubscriptionCollection : PagedCollection<OptOut>
    {
        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<OptOut> instance that contains the optouts.]]>
        /// </summary>
        [XmlElement("optout")]
        public List<OptOut> OptOuts
        {
            get { return Items; }
            set { Items = value; }
        }
    }
}