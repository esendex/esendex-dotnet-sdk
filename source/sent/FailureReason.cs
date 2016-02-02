using System.Xml.Serialization;

namespace com.esendex.sdk.sent
{
    /// <summary>
    /// Failure Reason information for a sent message
    /// </summary>
    public class FailureReason
    {
        /// <summary>
        /// Numerical code representing the failure reason.
        /// </summary>
        [XmlElement("code")]
        public int Code { get; set; }

        /// <summary>
        /// Can the message be retried or not.
        /// </summary>
        [XmlElement("permanentfailure")]
        public bool PermanentFailure { get; set; }

        /// <summary>
        /// Verbal representation of the failure reason.
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }
    }
}