using System;
using com.esendex.sdk.inbox;

namespace com.esendex.sdk.rest.resources
{
    internal class InboxMessagesResource : RestResource
    {
        public override string ResourceName
        {
            get { return "inbox"; }
        }

        public override string ResourceVersion
        {
            get { return "v1.1"; }
        }

        public InboxMessagesResource()
        {
            ResourcePath += "/messages";
        }

        public InboxMessagesResource(Guid id)
            : this()
        {
            ResourcePath += string.Format("/{0}", id);
        }

        public InboxMessagesResource(int pageNumber, int pageSize)
            : this()
        {
            Initialise(pageNumber, pageSize);
        }

        public InboxMessagesResource(string accountReference)
        {
            Initialise(accountReference);
        }

        public InboxMessagesResource(string accountReference, int pageNumber, int pageSize)
        {
            Initialise(accountReference);
            Initialise(pageNumber, pageSize);
        }

        public InboxMessagesResource(Guid id, InboxMessageStatus status)
            : this()
        {
            ResourcePath += string.Format("/{0}?action={1}", id, status.ToString()
                                                                       .ToLowerInvariant());
        }

        private void Initialise(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw new ArgumentException("Page number must be greater than zero.", "pageNumber");
            if (pageSize < 1) throw new ArgumentException("Page size must be greater than zero.", "pageSize");

            var startIndex = ((--pageNumber)*pageSize);

            ResourcePath += string.Format("?startIndex={0}&count={1}", startIndex, pageSize);
        }

        private void Initialise(string accountReference)
        {
            if (string.IsNullOrEmpty(accountReference)) throw new ArgumentNullException("accountReference");

            ResourcePath += string.Format("/{0}/messages", accountReference);
        }
    }
}