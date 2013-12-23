using System;

namespace com.esendex.sdk.rest.resources
{
    internal class AccountsResource : RestResource
    {
        public override string ResourceName
        {
            get { return "accounts" ; }
        }

        public AccountsResource()
        {
        }

        public AccountsResource(Guid id) : base()
        {
            ResourcePath += string.Format("/{0}", id);
        }
    }
}