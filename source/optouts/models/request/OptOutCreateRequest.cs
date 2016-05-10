using com.esendex.sdk.optouts.models.response;

namespace com.esendex.sdk.optouts.models.request
{
    public class OptOutCreateRequest
    {
        public string AccountReference { get; set; }
        public FromAddress From { get; set; }
    }
}