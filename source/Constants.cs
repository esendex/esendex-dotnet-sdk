using System;

namespace com.esendex.sdk
{
    internal static class Constants
    {
        internal const string API_NAMESPACE = "http://api.esendex.com/ns/";

        internal static Uri api_uri;

        internal static Uri API_URI
        {
            get { return api_uri ?? (api_uri = new UriBuilder("https", "api.esendex.com").Uri); }
        }

        internal static string JSON_MEDIA_TYPE = "application/json; charset=utf-8";
    }
}