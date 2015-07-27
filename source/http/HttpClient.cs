using System;
using System.Net;
using System.Reflection;

namespace com.esendex.sdk.http
{
    internal class HttpClient : IHttpClient
    {
        private static readonly Version _version = Assembly.GetAssembly(typeof(IHttpRequestHelper)).GetName().Version;
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
                var webRequest = HttpRequestHelper.Create(request, Uri, _version);

                HttpRequestHelper.AddCredentials(webRequest, Credentials);

                HttpRequestHelper.AddProxy(webRequest, Credentials.WebProxy);

                HttpRequestHelper.AddContent(webRequest, request);

                var webResponse = webRequest.GetResponse();

                return HttpResponseHelper.Create(webResponse);
            }
            catch (WebException exception)
            {
                return HttpResponseHelper.Create(exception);
            }
        }
    }
}