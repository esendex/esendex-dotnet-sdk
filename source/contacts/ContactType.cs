
using System;
namespace com.esendex.sdk.contacts
{
    /// <summary>
    ///  Contains the values of contact types.
    /// </summary>
    [Serializable]
    public enum ContactType
    {
        /// <summary>
        /// An Esendex contact.
        /// </summary>
        Esendex,

        /// <summary>
        /// A contact synchronised from a mobile phone.
        /// </summary>
        Mobile
    }
}