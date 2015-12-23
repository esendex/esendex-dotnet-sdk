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
            if (credentials.UseSessionAuthentication)
            {
                var value = string.Format("Basic {0}", Convert.ToBase64String(new UTF8Encoding().GetBytes(credentials.SessionId.Value.ToString())));

                httpRequest.Headers.Add(HttpRequestHeader.Authorization, value);
            }
            else
            {
                var credentialCache = new CredentialCache
                {
                    {
                        httpRequest.RequestUri,
                        "Basic",
                        new NetworkCredential(credentials.Username, credentials.Password)
                    }
                };

                httpRequest.Credentials = credentialCache;
            }
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