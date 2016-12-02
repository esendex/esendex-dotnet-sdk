namespace com.esendex.sdk.surveys.models
{
    /// <summary>
    /// An error containing a code, description and an array of values associated with the error.
    /// </summary>
    public class SurveySendError
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string[] Values { get; set; }
    }
}