using System.Net;
using com.esendex.sdk.results;

namespace com.esendex.sdk.exceptions
{
    public class BadRequestException : WebException
    {
        public BadRequestException(WebException webException, Error[] errors) 
            : base(webException.Message, webException.InnerException, webException.Status, webException.Response)
        {
            Errors = errors;
        }

        public Error[] Errors { get; private set; }
    }
}