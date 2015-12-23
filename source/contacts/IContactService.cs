using System;

namespace com.esendex.sdk.contacts
{
    /// <summary>
    /// Defines methods to manage contacts.
    /// </summary>
    public interface IContactService
    {
        /// <summary>
        /// Creates a com.esendex.sdk.contacts.Contact instance and returns the new com.esendex.sdk.contacts.Contact instance.        
        /// </summary>
        /// <param name="contact">A com.esendex.sdk.contacts.Contact instance that contains the contact.</param>
        /// <returns>A com.esendex.sdk.contacts.Contact instance that contains the contact with an Id assigned.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>        
        Contact CreateContact(Contact contact);

        /// <summary>
        /// Returns true if the contact was successfully deleted; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a contact.</param>
        /// <returns>true, if the contact was successfully deleted; otherwise, false.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        bool DeleteContact(Guid id);

        /// <summary>
        /// Returns true if the contact was successfully updated; otherwise, false.
        /// </summary>
        /// <param name="contact">A com.esendex.sdk.contacts.Contact instance that contains the contact.</param>
        /// <returns>true, if the contact was successfully updated; otherwise, false.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>       
        bool UpdateContact(Contact contact);

        /// <summary>
        /// Gets a com.esendex.sdk.contact.Contact instance containing a contact.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a contact.</param>
        /// <returns>A com.esendex.sdk.contacts.Contact instance that contains the contact.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        Contact GetContact(Guid id);

        /// <summary>
        /// Gets a com.esendex.sdk.contact.PagedContactCollection instance containing contacts.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.contacts.PagedContactCollection instance that contains the contacts.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        PagedContactCollection GetContacts(string accountReference, int pageNumber, int pageSize);
    }
}