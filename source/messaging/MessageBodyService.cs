using com.esendex.sdk.core;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Retrieves message text for a message body
    /// </summary>
    public class MessageBodyService : ServiceBase
    {
        public MessageBodyService(string username, string password)
            : this(new EsendexCredentials(username, password))
        {
        }

        public MessageBodyService(EsendexCredentials credentials)
            : base(credentials)
        {
        }

        internal MessageBodyService(IRestClient restClient, ISerialiser serialiser)
            : base(restClient, serialiser)
        {
        }

        /// <summary>
        /// Load and append the text to the given MessageBody
        /// </summary>
        /// <param name="messageBody"></param>
        public void LoadBodyText(MessageBody messageBody)
        {
            var resource = new ResourceLinkResource(messageBody);

            var response = MakeRequest<MessageBody>(HttpMethod.GET, resource);

            messageBody.BodyText = response.BodyText;
        }
    }
}