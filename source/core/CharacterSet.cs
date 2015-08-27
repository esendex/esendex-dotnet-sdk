using System;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Contains the allowed character sets.
    /// </summary>
    [Serializable]
    public enum CharacterSet
    {
        /// <summary>
        /// Default value does not set characterset element.
        /// </summary>
        Default,

        /// <summary>
        /// Auto detect characterset.
        /// </summary>
        Auto,

        /// <summary>
        /// GSM Character Set.
        /// </summary>
        GSM,

        /// <summary>
        /// Unicode Character Set.
        /// </summary>
        Unicode
    }
}