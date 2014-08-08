using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using com.esendex.sdk.adapters;

namespace com.esendex.sdk.http
{
    internal class HttpRequestHelper : IHttpRequestHelper
    {
        public IHttpWebRequestAdapter Create(HttpRequest httpRequest, Uri uri)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(IHttpRequestHelper));

            Version version = assembly.GetName().Version;

            string fullUri = string.Format("{0}v{1}.{2}/{3}", uri, version.Major, version.Minor, httpRequest.ResourcePath);

            uri = new Uri(fullUri);

            IHttpWebRequestAdapter httpWebRequest = new HttpWebRequestAdapter(uri);

            httpWebRequest.Method = httpRequest.HttpMethod.ToString();

            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);

            httpWebRequest.UserAgent = string.Format("Esendex .NET SDK v{0}.{1}", info.FileMajorPart, info.FileMinorPart);

            return httpWebRequest;
        }

        public void AddCredentials(IHttpWebRequestAdapter httpRequest, EsendexCredentials credentials)
        {
            if (credentials.UseSessionAuthentication)
            {
                string value = string.Format("Basic {0}", Convert.ToBase64String(new UTF8Encoding().GetBytes(credentials.SessionId.Value.ToString())));

                httpRequest.Headers.Add(HttpRequestHeader.Authorization, value);
            }
            else
            {
                CredentialCache credentialCache = new CredentialCache();

                credentialCache.Add(httpRequest.RequestUri, "Basic", new NetworkCredential(credentials.Username, credentials.Password));

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

                byte[] data = request.ContentEncoding.GetBytes(request.Content);

                using (Stream stream = httpRequest.GetRequestStream())
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
