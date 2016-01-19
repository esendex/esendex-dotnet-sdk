using System;
using System.Collections.Specialized;
using System.IO;
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
            byte[] b;
            using (var stream = inputStream)
            using (var ms = new MemoryStream())
            {
                int count;
                do
                {
                    var buf = new byte[1024];
                    count = stream.Read(buf, 0, 1024);
                    ms.Write(buf, 0, count);
                } while (stream.CanRead && count > 0);
                b = ms.ToArray();
            }
            using (var dataStream = new MemoryStream(b))
            using (var reader = new StreamReader(dataStream))
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