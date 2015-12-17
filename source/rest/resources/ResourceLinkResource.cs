using System;
using com.esendex.sdk.core;

namespace com.esendex.sdk.rest.resources
{
    internal class ResourceLinkResource : RestResource
    {
        public ResourceLinkResource(ResourceLink resourceLink)
        {
            ResourcePath = new Uri(resourceLink.Uri).PathAndQuery;
        }

        public override string ResourceName
        {
            get { return string.Empty; }
        }

        public override string ResourceVersion
        {
            get { return "v1.1"; }
        }
    }
}