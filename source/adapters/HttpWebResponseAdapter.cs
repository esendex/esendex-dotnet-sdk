using System;
using System.IO;
using System.Net;

namespace com.esendex.sdk.adapters
{
    internal class HttpWebResponseAdapter : IHttpWebResponseAdapter
    {
        private HttpWebResponse httpWebResponse;

        public HttpWebResponseAdapter(HttpWebResponse response)
        {
            httpWebResponse = response;
        }

        public Stream GetResponseStream()
        {
            return httpWebResponse.GetResponseStream();
        }

        public Int64 ContentLength
        {
            get { return httpWebResponse.ContentLength; }
            set { httpWebResponse.ContentLength = value; }
        }

        public string ContentType
        {
            get { return httpWebResponse.ContentType; }
            set { httpWebResponse.ContentType = value; }
        }

        public HttpStatusCode StatusCode
        {
            get { return httpWebResponse.StatusCode; }
        }

        public string ContentEncoding
        {
            get { return httpWebResponse.ContentEncoding; }
        }
    }
}