using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;

namespace com.esendex.sdk.test.mockapi
{
    public class Request
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public NameValueCollection Headers { get; set; }
    }

    public class MockApi
    {
        public static string Url { get; set; }
        private static HttpListener _listener;
        public static Request LastRequest;
        private static MockEndpoint _endpoint;

        public static void Start(string host)
        {
            Url = string.Format("http://{0}/", host);

            _listener = new HttpListener();
            _listener.Prefixes.Add(Url);

            _listener.Start();
            _listener.BeginGetContext(HandleRequests, null);
        }

        public static void SetEndpoint(MockEndpoint endpoint)
        {
            _endpoint = endpoint;
        }

        private static void HandleRequests(IAsyncResult asyncResult)
        {
            try
            {
                if (!_listener.IsListening)
                    return;

                var context = _listener.EndGetContext(asyncResult);

                LastRequest = new Request
                {
                    Body = ExtractRequestBody(context.Request.InputStream),
                    Headers = context.Request.Headers,
                    Url = context.Request.Url.PathAndQuery,
                    Method = context.Request.HttpMethod
                };

#if NET35
                // .NET 3.5 doesn't pass User-Agent through the headers (it pulls it out and populates .UserAgent instead)
                // But .NET 4.0+ does. So if we're under .NET 3.5 we add it manually.
                if (!LastRequest.Headers.AllKeys.Contains("User-Agent"))
                {
                    LastRequest.Headers.Add("User-Agent", context.Request.UserAgent);
                }
#endif

                _endpoint.HandleRequest(context);
            }
            catch (HttpListenerException) { }
            finally
            {
                try
                {
                    _listener.BeginGetContext(HandleRequests, null);
                }
                catch (InvalidOperationException) { }
            }
        }

        private static string ExtractRequestBody(Stream inputStream)
        {
            using (var reader = new StreamReader(inputStream))
            {
                return reader.ReadToEnd();
            }
        }

        public static void Stop()
        {
            _listener.Stop();
        }
    }
}