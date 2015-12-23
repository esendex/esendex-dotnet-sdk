namespace com.esendex.sdk.rest
{
    internal interface IRestClient
    {
        RestResponse Get(RestResource resource);
        RestResponse Post(RestResource resource);
        RestResponse Put(RestResource resource);
        RestResponse Delete(RestResource resource);
    }
}