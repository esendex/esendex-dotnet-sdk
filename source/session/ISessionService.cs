using System;

namespace com.esendex.sdk.session
{
    /// <summary>
    /// Defines operations that enable session management.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Creates a System.Guid instance that contains the session id.
        /// </summary>
        /// <returns>A System.Guid instance that contains the session id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        Guid CreateSession();
    }
}