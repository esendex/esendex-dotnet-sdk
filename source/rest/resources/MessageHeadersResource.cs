using System;

namespace com.esendex.sdk.rest.resources
{
    internal class MessageHeadersResource : RestResource
    {
        public override string ResourceName
        {
            get { return "messageheaders"; }
        }

        public override string ResourceVersion
        {
            get { return "v1.1"; }
        }

        public MessageHeadersResource(Guid id)
            : base()
        {
            ResourcePath += string.Format("/{0}", id);
        }

        public MessageHeadersResource(int pageNumber, int pageSize)
            : base()
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");

            int startIndex = ((--pageNumber)*pageSize);

            ResourcePath += string.Format("?startIndex={0}&count={1}", startIndex, pageSize);
        }


        public MessageHeadersResource(string accountReference, int pageNumber, int pageSize)
            : this(pageNumber, pageSize)
        {
            if (string.IsNullOrEmpty(accountReference)) throw new ArgumentNullException("accountReference");

            ResourcePath += string.Format("&filterBy=account&filterValue={0}", accountReference);
        }

        public override bool Equals(object obj)
        {
            MessageHeadersResource other = obj as MessageHeadersResource;

            if (other == null) return false;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
