using System;
using System.Net;
using System.Text;
using com.esendex.sdk.adapters;

namespace com.esendex.sdk.http
{
    internal class HttpRequestHelper : IHttpRequestHelper
    {
        public IHttpWebRequestAdapter Create(HttpRequest httpRequest, Uri uri, Version version)
        {
            var fullUri = string.Format("{0}{1}/{2}", uri, httpRequest.ResourceVersion, httpRequest.ResourcePath);
            uri = new Uri(fullUri);

            IHttpWebRequestAdapter httpWebRequest = new HttpWebRequestAdapter(uri);
            httpWebRequest.Method = httpRequest.HttpMethod.ToString();
            httpWebRequest.UserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", version.Major, version.Minor, version.Build);

            return httpWebRequest;
        }

        public void AddCredentials(IHttpWebRequestAdapter httpRequest, EsendexCredentials credentials)
        {
            var authBytes = credentials.UseSessionAuthentication
                ? new UTF8Encoding().GetBytes(credentials.SessionId.Value.ToString())
                : new UTF8Encoding().GetBytes(string.Format("{0}:{1}", credentials.Username, credentials.Password));

            var value = string.Format("Basic {0}", Convert.ToBase64String(authBytes));
            httpRequest.Headers.Add(HttpRequestHeader.Authorization, value);
        }

        public void AddProxy(IHttpWebRequestAdapter httpRequest, IWebProxy proxy)
        {
            httpRequest.Proxy = proxy;
        }

        public void AddContent(IHttpWebRequestAdapter httpRequest, HttpRequest request)
        {
            if (request.HasContent)
            {
                httpRequest.ContentType = request.ContentType;
                httpRequest.ContentLength = request.ContentLength;

                var data = request.ContentEncoding.GetBytes(request.Content);

                using (var stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                httpRequest.ContentLength = 0;
            }
        }
    }
}