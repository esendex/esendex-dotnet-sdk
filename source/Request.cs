using System;
using System.IO;
using System.Net;
using System.Reflection;
using com.esendex.sdk.surveys;

namespace com.esendex.sdk
{
    internal class Request
    {
        private readonly Version _version = Assembly.GetAssembly(typeof (SurveysService)).GetName().Version;
        private readonly HttpWebRequest _request;

        private Request(string method, string url)
        {
            _request = (HttpWebRequest) WebRequest.Create(url);
            _request.Method = method;
            _request.UserAgent = string.Format("Esendex .NET SDK v{0}.{1}.{2}", _version.Major, _version.Minor, _version.Build);
        }

        public static Request Create(string method, string url)
        {
            return new Request(method, url);
        }

        public Request WithAcceptHeader(string acceptHeader)
        {
            _request.Accept = acceptHeader;
            return this;
        }

        public Request WithHeader(string key, string value)
        {
            _request.Headers.Add(key, value);
            return this;
        }

        public Request If(bool p, Func<Request, Request> f)
        {
            return p ? f(this) : this;
        }

        public Request WithProxy(IWebProxy webProxy)
        {
            _request.Proxy = webProxy;
            return this;
        }

        public Request WriteBody(string contentType, Action<StreamWriter> writer)
        {
            _request.ContentType = contentType;

            using (var requestStream = _request.GetRequestStream())
            using (var streamWriter = new StreamWriter(requestStream))
            {
                writer(streamWriter);
            }

            return this;
        }

        public HttpWebResponse GetResponse()
        {
            return (HttpWebResponse) _request.GetResponse();
        }
    }
}