namespace com.esendex.sdk.rest.resources
{
    internal class SessionResource : RestResource
    {
        public override string ResourceName
        {
            get { return "session"; }
        }

        public override string ResourceVersion
        {
            get { return "v1.1"; }
        }

        public SessionResource()
        {
            ResourcePath += "/constructor";
        }
    }
}