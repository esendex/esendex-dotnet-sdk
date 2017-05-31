using System;
using com.esendex.sdk.contacts;

namespace com.esendex.sdk.rest.resources
{
    internal class GroupsResource : RestResource
    {
        public override string ResourceName
        {
            get { return "contactgroups"; }
        }

        public override string ResourceVersion
        {
            get { return "v2.0"; }
        }

        public GroupsResource(Guid id)
        {
            AppendWithId(id);
        }

        public GroupsResource(string content)
            : base(content)
        {
        }

        public GroupsResource(Guid id, string content)
            : base(content)
        {
            AppendWithId(id);
        }

        public GroupsResource(string accountReference, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");

            var startIndex = ((--pageNumber)*pageSize);

            ResourcePath += string.Format("?accountReference={0}&startIndex={1}&count={2}", accountReference, startIndex, pageSize);
        }

        public GroupsResource(string accountReference, string groupId, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");
            var startIndex = ((--pageNumber) * pageSize);

            ResourcePath += string.Format("/{0}/contacts?accountReference={1}&startIndex={2}&count={3}", groupId, accountReference, startIndex, pageSize);
        }

        public GroupsResource(string accountReference, string groupId, string content)
            :base(content)
        {
            ResourcePath += string.Format("/{0}/contacts?accountReference={1}", groupId, accountReference);
        }

        private void AppendWithId(Guid id)
        {
            ResourcePath += string.Format("/{0}", id);
        }
    }
}