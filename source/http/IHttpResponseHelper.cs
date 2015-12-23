using System.Net;
using com.esendex.sdk.adapters;

namespace com.esendex.sdk.http
{
    internal interface IHttpResponseHelper
    {
        HttpResponse Create(IHttpWebResponseAdapter response);
        HttpResponse Create(WebException exception);
    }
}