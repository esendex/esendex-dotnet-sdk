using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace com.esendex.sdk.extensions
{
    internal static class HttpWebResponseExtensions
    {
        public static T DeserialiseJson<T>(this HttpWebResponse response)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(response.GetResponseStream()))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<T>(jsonTextReader);
            }
        }
    }
}