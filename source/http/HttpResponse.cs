using System.Net;

namespace com.esendex.sdk.http
{
    internal class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }

        public long ContentLength
        {
            get { return Content.Length; }
        }

        public string Content { get; set; }
        public string ContentEncoding { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as HttpResponse;

            if (other == null) return false;

            if (StatusCode != other.StatusCode) return false;
            if (ContentType != other.ContentType) return false;
            if (ContentLength != other.ContentLength) return false;
            if (Content != other.Content) return false;
            if (ContentEncoding != other.ContentEncoding) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}