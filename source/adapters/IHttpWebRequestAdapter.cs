using System;
using System.IO;
using System.Net;

namespace com.esendex.sdk.adapters
{
    internal interface IHttpWebRequestAdapter
    {
        long ContentLength { get; set; }
        string ContentType { get; set; }
        Stream GetRequestStream();
        IHttpWebResponseAdapter GetResponse();
        string Method { get; set; }
        IWebProxy Proxy { get; set; }
        ICredentials Credentials { get; set; }
        string UserAgent { get; set; }
        Uri RequestUri { get; }
        WebHeaderCollection Headers { get; set; }
    }
}