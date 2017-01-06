using System;

namespace com.esendex.sdk.rest.resources
{
    internal class MessageBatchesResource : RestResource
    {
        public override string ResourceName
        {
            get { return "messagebatches"; }
        }

        public override string ResourceVersion
        {
            get { return "v1.0"; }
        }

        public MessageBatchesResource(Guid id) 
        {
            ResourcePath += string.Format("/{0}/messages", id);
        }

        public override bool Equals(object obj)
        {
            var other = obj as MessageBatchesResource;

            if (other == null) return false;

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
