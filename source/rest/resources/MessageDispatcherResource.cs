
namespace com.esendex.sdk.rest.resources
{
    internal class MessageDispatcherResource : RestResource
    {
        public override string ResourceName { get { return "messagedispatcher"; } }

        public MessageDispatcherResource(string content, bool ensureMessageIdsInResult)
            : base(content)
        {
            if (ensureMessageIdsInResult)
            {
                ResourcePath += "?returnmessageheaders=true";
            }
        }

        public override bool Equals(object obj)
        {
            MessageDispatcherResource other = obj as MessageDispatcherResource;

            if (other == null) return false;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}