namespace com.esendex.sdk.optouts.models.response
{
    /// <summary>
    /// Represents the result of attempting to creat a new opt out
    /// </summary>
    public class OptOutCreateResult
    {
        /// <summary>
        /// <![CDATA[Initialises a new instance of the com.esendex.sdk.optouts.models.response.OptOutCreateResult<T>]]>
        /// </summary>
        public OptOutCreateResult()
        {
            Errors = new OptOutCreateError[0];
        }

        /// <summary>
        /// <![CDATA[A com.esendex.sdk.optouts.models.response.OptOut instance that contains the created opt out if it was successful.]]>
        /// </summary>
        public OptOut OptOut { get; set; }

        /// <summary>
        /// <![CDATA[A com.esendex.sdk.optouts.models.response.OptOutCreateError instance that contains errors associated with a failed attempt.]]>
        /// </summary>
        public OptOutCreateError[] Errors { get; set; }
    }
}