using System;
using System.IO;
using System.Net;

namespace com.esendex.sdk.adapters
{
    internal class HttpWebRequestAdapter : IHttpWebRequestAdapter
    {
        private HttpWebRequest httpWebRequest;

        public HttpWebRequestAdapter(Uri url)
        {
            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
        }

        public Stream GetRequestStream()
        {
            return httpWebRequest.GetRequestStream();
        }

        public IHttpWebResponseAdapter GetResponse()
        {
            return new HttpWebResponseAdapter((HttpWebResponse)httpWebRequest.GetResponse());
        }

        public Int64 ContentLength
        {
            get { return httpWebRequest.ContentLength; }
            set { httpWebRequest.ContentLength = value; }
        }

        public string Method 
        {
            get { return httpWebRequest.Method; }
            set { httpWebRequest.Method = value; }
        }

        public string ContentType
        {
            get { return httpWebRequest.ContentType; }
            set { httpWebRequest.ContentType = value; }
        }

        public IWebProxy Proxy
        {
            get { return httpWebRequest.Proxy; }
            set { httpWebRequest.Proxy = value; }
        }

        public ICredentials Credentials
        {
            get { return httpWebRequest.Credentials; }
            set { httpWebRequest.Credentials = value; }
        }

        public string UserAgent
        {
            get { return httpWebRequest.UserAgent; }
            set { httpWebRequest.UserAgent = value; }
        }

        public Uri RequestUri
        {
            get { return httpWebRequest.RequestUri; }
        }

        public WebHeaderCollection Headers
        {
            get { return httpWebRequest.Headers; }
            set { httpWebRequest.Headers = value; }
        }
    }
}