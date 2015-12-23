using System;

namespace com.esendex.sdk.rest.resources
{
    internal class ContactsResource : RestResource
    {
        public override string ResourceName
        {
            get { return "contacts"; }
        }

        public override string ResourceVersion
        {
            get { return "v2.0"; }
        }

        public ContactsResource(Guid id)
        {
            AppendWithId(id);
        }

        public ContactsResource(string content)
            : base(content)
        {
        }

        public ContactsResource(Guid id, string content)
            : base(content)
        {
            AppendWithId(id);
        }

        public ContactsResource(string accountReference, int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");

            var startIndex = ((--pageNumber)*pageSize);

            ResourcePath += string.Format("?accountReference={0}&startIndex={1}&count={2}", accountReference, startIndex, pageSize);
        }

        private void AppendWithId(Guid id)
        {
            ResourcePath += string.Format("/{0}", id);
        }
    }
}