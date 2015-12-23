using System.Text;
using com.esendex.sdk.http;

namespace com.esendex.sdk.rest
{
    internal class RestClient : IRestClient
    {
        public IHttpClient HttpClient { get; set; }

        public RestClient(IHttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public RestResponse Post(RestResource resource)
        {
            var request = new HttpRequest
            {
                Content = resource.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8,
                ResourcePath = resource.ResourcePath,
                ResourceVersion = resource.ResourceVersion,
                HttpMethod = HttpMethod.POST
            };

            var response = HttpClient.Submit(request);

            if (response == null) return null;

            return new RestResponse
            {
                StatusCode = response.StatusCode,
                Content = response.Content
            };
        }

        public RestResponse Get(RestResource resource)
        {
            var request = new HttpRequest
            {
                ContentType = "text/plain",
                ResourcePath = resource.ResourcePath,
                ResourceVersion = resource.ResourceVersion,
                HttpMethod = HttpMethod.GET
            };

            var response = HttpClient.Submit(request);

            if (response == null) return null;

            return new RestResponse
            {
                StatusCode = response.StatusCode,
                Content = response.Content
            };
        }


        public RestResponse Put(RestResource resource)
        {
            var request = new HttpRequest
            {
                Content = resource.Content,
                ContentType = "application/xml",
                ContentEncoding = Encoding.UTF8,
                ResourcePath = resource.ResourcePath,
                ResourceVersion = resource.ResourceVersion,
                HttpMethod = HttpMethod.PUT
            };

            var response = HttpClient.Submit(request);

            if (response == null) return null;

            return new RestResponse
            {
                StatusCode = response.StatusCode,
                Content = response.Content
            };
        }

        public RestResponse Delete(RestResource resource)
        {
            var request = new HttpRequest
            {
                ContentType = "text/plain",
                ResourcePath = resource.ResourcePath,
                ResourceVersion = resource.ResourceVersion,
                HttpMethod = HttpMethod.DELETE
            };

            var response = HttpClient.Submit(request);

            if (response == null) return null;

            return new RestResponse
            {
                StatusCode = response.StatusCode,
                Content = response.Content
            };
        }
    }
}