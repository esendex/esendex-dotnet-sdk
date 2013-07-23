
using System;
using System.Net;
using com.esendex.sdk.adapters;
namespace com.esendex.sdk.http
{
    internal class HttpClient : IHttpClient
    {
        public IHttpRequestHelper HttpRequestHelper { get; private set; }
        public IHttpResponseHelper HttpResponseHelper { get; private set; }
        public EsendexCredentials Credentials { get; private set; }
        public Uri Uri { get; private set; }

        public HttpClient(EsendexCredentials credentials, Uri uri, IHttpRequestHelper httpRequestHelper, IHttpResponseHelper httpResponseHelper)
        {
            HttpRequestHelper = httpRequestHelper;
            HttpResponseHelper = httpResponseHelper;
            Credentials = credentials;
            Uri = uri;
        }

        public HttpResponse Submit(HttpRequest request)
        {
            try
            {
                IHttpWebRequestAdapter webRequest = HttpRequestHelper.Create(request, Uri);

                HttpRequestHelper.AddCredentials(webRequest, Credentials);

                HttpRequestHelper.AddProxy(webRequest, Credentials.WebProxy);

                HttpRequestHelper.AddContent(webRequest, request);

                IHttpWebResponseAdapter webResponse = (IHttpWebResponseAdapter)webRequest.GetResponse();

                return HttpResponseHelper.Create(webResponse);
            }
            catch (WebException exception)
            {
                return HttpResponseHelper.Create(exception);
            }
        }
    }
}