using System;

namespace com.esendex.sdk.optouts.models.response
{
    /// <summary>
    /// Represents an opt out
    /// </summary>
    public class OptOut
    {
        /// <summary>
        /// <![CDATA[A System.Guid instance that identifies the opt out.]]>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// <![CDATA[A System.DateTime instance that indicates when the opt out was created.]]>
        /// </summary>
        public DateTime ReceivedAt { get; set; }

        /// <summary>
        /// <![CDATA[A System.String instance that indicates the account the opt out belongs to.]]>
        /// </summary>
        public string AccountReference { get; set; }

        /// <summary>
        /// <![CDATA[A com.esendex.sdk.optouts.models.response.FromAddress instance that represents the address of the opt out.]]>
        /// </summary>
        public FromAddress From { get; set; }
    }
}