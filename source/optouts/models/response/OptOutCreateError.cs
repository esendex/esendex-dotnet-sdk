namespace com.esendex.sdk.optouts.models.response
{
    /// <summary>
    /// Holds information related to a bad request response when creating a new opt out
    /// </summary>
    public class OptOutCreateError
    {
        /// <summary>
        /// <![CDATA[A System.String instance that contains the code indicating the failure reason.]]>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// <![CDATA[A System.String instance that contains a human readable description of the failure reason.]]>
        /// </summary>
        public string Description { get; set; }
    }
}