using System.Collections.Generic;
using com.esendex.sdk.core;

namespace com.esendex.sdk.optouts.models.response
{
    /// <summary>
    /// Represents a collection of Opt Outs
    /// </summary>
    public class OptOutCollection : PagedCollection<OptOut>
    {
        /// <summary>
        /// <![CDATA[A System.Collections.Generic.List<OptOut> instance that contains the optouts.]]>
        /// </summary>
        public List<OptOut> OptOuts
        {
            get { return Items; }
            set { Items = value; }
        }
    }
}