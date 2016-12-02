namespace com.esendex.sdk.surveys.models
{
    /// <summary>
    /// An enum representing what to filter report rows on.
    /// </summary>
    public enum DateRangeType
    {
        /// <summary>
        /// Filter report rows by the date questions were sent
        /// </summary>
        QuestionSent,

        /// <summary>
        /// Filter report rows by the date answers were received
        /// </summary>
        AnswerReceived
    }
}