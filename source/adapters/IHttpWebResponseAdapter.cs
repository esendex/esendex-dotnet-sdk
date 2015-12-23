using System.IO;
using System.Net;

namespace com.esendex.sdk.adapters
{
    internal interface IHttpWebResponseAdapter
    {
        HttpStatusCode StatusCode { get; }
        long ContentLength { get; set; }
        string ContentType { get; set; }
        Stream GetResponseStream();
        string ContentEncoding { get; }
    }
}