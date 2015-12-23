using System.Text;

namespace com.esendex.sdk.http
{
    internal enum HttpMethod
    {
        DELETE,
        GET,
        POST,
        PUT
    }

    internal class HttpRequest
    {
        public HttpRequest()
        {
            ResourceVersion = "v1.1";
            ContentEncoding = Encoding.UTF8;
        }

        public string ResourcePath { get; set; }
        public string ResourceVersion { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public Encoding ContentEncoding { get; set; }
        public string ContentType { get; set; }
        public string Content { get; set; }

        public long ContentLength
        {
            get { return Content == null ? 0 : ContentEncoding.GetByteCount(Content); }
        }

        public bool HasContent
        {
            get { return (ContentLength > 0 && !string.IsNullOrEmpty(Content)); }
        }

        public override bool Equals(object obj)
        {
            var other = obj as HttpRequest;

            if (other == null) return false;

            if (Content != other.Content) return false;
            if (ContentEncoding != other.ContentEncoding) return false;
            if (ContentLength != other.ContentLength) return false;
            if (ContentType != other.ContentType) return false;
            if (HasContent != other.HasContent) return false;
            if (HttpMethod != other.HttpMethod) return false;
            if (ResourcePath != other.ResourcePath) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}