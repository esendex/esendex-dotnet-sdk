
namespace com.esendex.sdk.rest.resources
{
    internal class SessionResource : RestResource
    {
        public override string ResourceName
        {
            get { return "session"; }
        }

        public SessionResource()
            : base()
        {
            ResourcePath += "/constructor";
        }
    }
}