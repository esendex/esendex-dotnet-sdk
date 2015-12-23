namespace com.esendex.sdk.http
{
    internal interface IHttpClient
    {
        HttpResponse Submit(HttpRequest request);
    }
}