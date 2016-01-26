using System.Net;
using System.Text;

namespace com.esendex.sdk.test.mockapi
{
    public class MockEndpoint
    {
        private readonly int _statusCode;
        private readonly string _body;
        private readonly string _contentType;

        public MockEndpoint(int statusCode, string body = "", string contentType = "")
        {
            _statusCode = statusCode;
            _body = body;
            _contentType = contentType;
        }

        public void HandleRequest(HttpListenerContext context)
        {
            context.Response.Headers.Add(HttpResponseHeader.ContentType, _contentType);
            context.Response.StatusCode = _statusCode;
            context.Response.Close(Encoding.UTF8.GetBytes(_body), false);
        }
    }
}