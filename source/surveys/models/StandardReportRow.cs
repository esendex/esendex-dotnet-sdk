using System;
using System.Collections.Generic;

namespace com.esendex.sdk.surveys.models
{
    /// <summary>
    /// Represents a question sent to a recipient and the answer received.
    /// </summary>
    public class StandardReportRow
    {
        /// <summary>
        /// A string containing the phone number of a recipient.
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// A DeliveryStatus representing the delivery status of the question sent.
        /// </summary>
        public DeliveryStatus Status { get; set; }

        /// <summary>
        /// A string containing the label of the question.
        /// </summary>
        public string QuestionLabel { get; set; }

        /// <summary>
        /// A DateTime representing when the question was sent.
        /// </summary>
        public DateTime QuestionDateTime { get; set; }

        /// <summary>
        /// A string containing the label of the answer received.
        /// </summary>
        public string AnswerLabel { get; set; }

        /// <summary>
        /// A nullable DateTime representing when the answer was received.
        /// </summary>
        public DateTime? AnswerDateTime { get; set; }

        /// <summary>
        /// A string containing the recipient's response to the question.
        /// </summary>
        public string AnswerText { get; set; }

        /// <summary>
        /// A dictionary containing both the template fields and metadata saved when adding the recipient to the survey.
        /// </summary>
        public Dictionary<string, string> RecipientData { get; set; }

    }
}