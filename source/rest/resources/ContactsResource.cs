using System;

namespace com.esendex.sdk.rest.resources
{
    internal class ContactsResource : RestResource
    {
        public override string ResourceName
        {
            get { return "contacts" ; }
        }

        public ContactsResource(Guid id)
            : base()
        {
            AppendWithId(id);
        }

        public ContactsResource(string content) 
            : base(content) { }

        public ContactsResource(Guid id, string content)
            : base(content)
        {
            AppendWithId(id);            
        }

        public ContactsResource(int pageNumber, int pageSize) 
            : base()           
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");

            int startIndex = ((--pageNumber) * pageSize);

            ResourcePath += string.Format("?startIndex={0}&count={1}", startIndex, pageSize);
        }

        private void AppendWithId(Guid id)
        {
            ResourcePath += string.Format("/{0}", id);
        }
    }
}