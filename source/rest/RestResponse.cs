using System.Net;

namespace com.esendex.sdk.rest
{
    internal class RestResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public long ContentLength
        {
            get { return Content.Length; }
        }

        public string Content { get; set; }
    }
}