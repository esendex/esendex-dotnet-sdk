using System;
using System.Net;
using com.esendex.sdk.adapters;

namespace com.esendex.sdk.http
{
    internal interface IHttpRequestHelper
    {
        IHttpWebRequestAdapter Create(HttpRequest httpRequest, Uri uri);
        void AddCredentials(IHttpWebRequestAdapter httpRequest, EsendexCredentials credentials);
        void AddProxy(IHttpWebRequestAdapter httpRequest, IWebProxy proxy);
        void AddContent(IHttpWebRequestAdapter httpRequest, HttpRequest request);
    }
}
