using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Contains the values of supported voice languages 
    /// </summary>
    [Serializable]
    public enum VoiceMessageLanguage
    {
        /// <summary>
        /// English
        /// </summary>
        [XmlEnum("en-GB")] en_GB,

        /// <summary>
        /// English (Austrailian)
        /// </summary>
        [XmlEnum("en-AU")] en_AU,

        /// <summary>
        /// Spanish
        /// </summary>
        [XmlEnum("es-ES")] es_ES,

        /// <summary>
        /// French
        /// </summary>
        [XmlEnum("fr-FR")] fr_FR,

        /// <summary>
        /// German
        /// </summary>
        [XmlEnum("de-DE")] de_DE
    }
}