using System;
using com.esendex.sdk.contacts;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.groups
{
    /// <summary>
    /// Defines methods to manage groups.
    /// </summary>
    public class GroupService : ServiceBase, IGroupService
    {
        /// <summary>
        /// Initialises a new instance of the GroupService
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        public GroupService(string username, string password)
            : this(new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Initialises a new instance of the com.esendex.sdk.groups.GroupService
        /// </summary>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance that contains access credentials.</param>
        public GroupService(EsendexCredentials credentials)
            : base(credentials)
        {
        }

        internal GroupService(IRestClient restClient, ISerialiser serialiser)
            : base(restClient, serialiser)
        {
        }

        /// <summary>
        /// Creates a com.esendex.sdk.groups.Group instance and returns the new com.esendex.sdk.groups.Group instance.
        /// </summary>
        /// <param name="group">A com.esendex.sdk.groups.Group instance that contains the group.</param>
        /// <returns>A com.esendex.sdk.groups.Group instance that contains the group with an Id assigned.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>        
        public Group CreateGroup(Group group)
        {
            var requestXml = Serialiser.Serialise(group);

            RestResource resource = new GroupsResource(requestXml);

            return MakeRequest<GroupResponse>(HttpMethod.POST, resource)
                .Group;
        }

        /// <summary>
        /// Returns true if the group was successfully deleted; otherwise, false.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a group.</param>
        /// <returns>true, if the group was successfully deleted; otherwise, false.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        public bool DeleteGroup(Guid id)
        {
            RestResource resource = new GroupsResource(id);

            var response = MakeRequest(HttpMethod.DELETE, resource);

            return (response != null);
        }

        /// <summary>
        /// Returns true if the group was successfully updated; otherwise, false.
        /// </summary>
        /// <param name="group">A com.esendex.sdk.groups.Group instance that contains the group.</param>
        /// <returns>true, if the group was successfully updated; otherwise, false.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>      
        public bool UpdateGroup(Group group)
        {
            var requestXml = Serialiser.Serialise(group);

            RestResource resource = new GroupsResource(group.Id, requestXml);

            var response = MakeRequest(HttpMethod.PUT, resource);

            return (response != null);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.group.Group instance containing a group.
        /// </summary>
        /// <param name="id">A System.Guid instance that contains the Id of a group.</param>
        /// <returns>A com.esendex.sdk.groups.Group instance that contains the group.</returns>        
        /// <exception cref="System.Net.WebException"></exception>
        public Group GetGroup(Guid id)
        {
            RestResource resource = new GroupsResource(id);

            return MakeRequest<Group>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.group.PagedGroupCollection instance containing groups.
        /// </summary>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.groups.PagedGroupCollection instance that contains the groups.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public PagedGroupCollection GetGroups(string accountReference, int pageNumber, int pageSize)
        {
            RestResource resource = new GroupsResource(accountReference, pageNumber, pageSize);

            return MakeRequest<PagedGroupCollection>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Gets a com.esendex.sdk.group.PagedGroupCollection instance containing groups.
        /// </summary>
        /// <param name="accountReference">The number of the page.</param>
        /// <param name="pageNumber">The number of the page.</param>
        /// <param name="pageSize">The number of items in the page.</param>
        /// <param name="groupId">The number of items in the page.</param>
        /// <returns>A com.esendex.sdk.groups.PagedGroupCollection instance that contains the groups.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public PagedContactCollection GetContactsFromGroup(string accountReference, string groupId, int pageNumber, int pageSize)
        {
            RestResource resource = new GroupsResource(accountReference, groupId, pageNumber, pageSize);

            return MakeRequest<PagedContactCollection>(HttpMethod.GET, resource);
        }

        /// <summary>
        /// Posts a com.esendex.sdk.contacts.Contact to a com.esendex.sdk.groups.Group.
        /// </summary>
        /// <param name="accountReference">The number of the page.</param>
        /// <param name="groupId">The number of items in the page.</param>
        /// <param name="contact"></param>
        /// <returns>A com.esendex.sdk.groups.PagedGroupCollection instance that contains the groups.</returns>        
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.Net.WebException"></exception>
        public bool AddContactToGroup(string accountReference, string groupId, Contact contact)
        {
            var contactColletion = new ContactCollection();
            contactColletion.ItemsId.Add(contact.Id.ToString());

            RestResource resource = new GroupsResource(accountReference, groupId, Serialiser.Serialise(contactColletion));
            var response = MakeRequest(HttpMethod.POST, resource);
            return response != null;
        }
    }
}