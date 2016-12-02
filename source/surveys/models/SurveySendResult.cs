namespace com.esendex.sdk.surveys.models
{
    /// <summary>
    /// A class containing an array of SurveySendErrors
    /// </summary>
    public class SurveySendResult
    {
        /// <summary>
        /// An array of SurveySendErrors
        /// </summary>
        public SurveySendError[] Errors { get; set; }
    }
}