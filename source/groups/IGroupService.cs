using System;

namespace com.esendex.sdk.groups
{
    /// <summary>
    /// Defines methods to manage contacts.
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Creates a com.esendex.sdk.contacts.Contact instance and returns the new com.esendex.sdk.contacts.Contact instance.        
        /// </summary>
        /// <param name="group">A com.esendex.sdk.contacts.Contact instance that contains the contact.</param>
        /// <returns>A com.esendex.sdk.contacts.Contact instance that contains the contact with an Id assigned.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>        
        Group CreateGroup(Group group);

        /// <summary>
        /// Returns true if the contact was successfully deleted; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a contact.</param>
        /// <returns>true, if the contact was successfully deleted; otherwise, false.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        bool DeleteGroup(Guid id);

        /// <summary>
        /// Returns true if the contact was successfully updated; otherwise, false.
        /// </summary>
        /// <param name="contact">A com.esendex.sdk.contacts.Contact instance that contains the contact.</param>
        /// <returns>true, if the contact was successfully updated; otherwise, false.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>       
        bool UpdateGroup(Group group);

        /// <summary>
        /// Gets a com.esendex.sdk.contact.Contact instance containing a contact.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a contact.</param>
        /// <returns>A com.esendex.sdk.contacts.Contact instance that contains the contact.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        Group GetGroup(Guid id);

        /// <summary>
        /// Gets a com.esendex.sdk.contact.PagedContactCollection instance containing contacts.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.contacts.PagedContactCollection instance that contains the contacts.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        PagedGroupCollection GetGroups(string accountReference, int pageNumber, int pageSize);
    }
}