namespace com.esendex.sdk.surveys.models
{
    /// <summary>
    /// A class containing an array of SurveyReportErrors.
    /// </summary>
    public class StandardReportResult
    {
        /// <summary>
        /// An array of StandardReportErrors.
        /// </summary>
        public SurveyReportError[] Errors { get; set; }
        /// <summary>
        /// An array of StandardReportRows.
        /// </summary>
        public StandardReportRow[] Rows { get; set; }
    }
}