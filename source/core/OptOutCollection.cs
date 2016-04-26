using System;
using System.Xml.Serialization;

namespace com.esendex.sdk.core
{
    /// <summary>
    /// Represents a collecion of optout messages.
    /// </summary>
    [Serializable]
    [XmlRoot("optouts", Namespace = Constants.API_NAMESPACE)]
    public class OptOutCollection : SubscriptionCollection
    {
    }
}