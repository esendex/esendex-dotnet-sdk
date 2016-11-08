using System;
using System.Collections.Specialized;
using System.Web;

namespace com.esendex.sdk.http
{
    public class HttpUriBuilder
    {
        private readonly UriBuilder _uriBuilder;
        private readonly NameValueCollection _query;

        private HttpUriBuilder(string url)
        {
            _uriBuilder = new UriBuilder(url);
            _query = HttpUtility.ParseQueryString(string.Empty);
        }

        public static HttpUriBuilder Create(string url)
        {
            return new HttpUriBuilder(url);
        }

        public HttpUriBuilder WithParameter(string name, string value)
        {
            _query[name] = value;
            return this;
        }

        public Uri Build()
        {
            _uriBuilder.Query = _query.ToString();
            return _uriBuilder.Uri;
        }
    }
}