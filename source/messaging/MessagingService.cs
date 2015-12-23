using System;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// A service to send messages.
    /// </summary>
    public class MessagingService : ServiceBase, IMessagingService
    {
        /// <summary>
        /// Gets a value indicating whether the service will return a collection of MessageHeader Ids in the MessagingResult.
        /// </summary>
        public bool EnsureMessageIdsInResult { get; private set; }

        /// <summary>
        /// Initialises a new instance of the MessagingService, defaults to not returning a collection of MessageHeader Ids in the MessagingResult
        /// </summary>
        /// <param name="username">Your Esendex username.</param>
        /// <param name="password">Your Esendex password.</param>
        public MessagingService(string username, string password)
            : this(false, new EsendexCredentials(username, password))
        {
        }

        /// <summary>
        /// Initialises a new instance of the MessagingService
        /// </summary>
        /// <param name="ensureMessageIdsInResult">true, if the service should return a collection of MessageHeader Ids in the MessagingResult; otherwise, false.</param>
        /// <param name="credentials">A com.esendex.sdk.EsendexCredentials instance that contains access credentials.</param>
        public MessagingService(bool ensureMessageIdsInResult, EsendexCredentials credentials)
            : base(credentials)
        {
            EnsureMessageIdsInResult = ensureMessageIdsInResult;
        }

        internal MessagingService(IRestClient restClient, ISerialiser serialiser, bool ensureMessageIdsInResult)
            : base(restClient, serialiser)
        {
            EnsureMessageIdsInResult = ensureMessageIdsInResult;
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessage instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.SmsMessage instance that contains the SMS message.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendMessage(SmsMessage message)
        {
            var messages = new SmsMessageCollection(message);

            return SendMessages<SmsMessageCollection>(messages);
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessage instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.VoiceMessage instance that contains the Voice message.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendMessage(VoiceMessage message)
        {
            var messages = new VoiceMessageCollection(message);

            return SendMessages<VoiceMessageCollection>(messages);
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessageCollection instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.SmsMessageCollection instance that contains an SMS message collection.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendMessages(SmsMessageCollection messages)
        {
            return SendMessages<SmsMessageCollection>(messages);
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessageCollection instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.VoiceMessageCollection instance that contains a Voice message collection.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendMessages(VoiceMessageCollection messages)
        {
            return SendMessages<VoiceMessageCollection>(messages);
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessage instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.SmsMessage instance that contains a SMS message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the message should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendScheduledMessage(SmsMessage message, DateTime sendAt)
        {
            var messages = new SmsMessageCollection(message) {SendAt = sendAt};

            return SendMessages<SmsMessageCollection>(messages);
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessage instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.VoiceMessage instance that contains a Voice message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the message should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendScheduledMessage(VoiceMessage message, DateTime sendAt)
        {
            var messages = new VoiceMessageCollection(message) {SendAt = sendAt};

            return SendMessages<VoiceMessageCollection>(messages);
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessageCollection instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.SmsMessageCollection instance that contains a SMS message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the messages should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendScheduledMessages(SmsMessageCollection messages, DateTime sendAt)
        {
            messages.SendAt = sendAt;

            return SendMessages<SmsMessageCollection>(messages);
        }

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessageCollection instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.VoiceMessageCollection instance that contains a Voice message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the messages should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        public MessagingResult SendScheduledMessages(VoiceMessageCollection messages, DateTime sendAt)
        {
            messages.SendAt = sendAt;

            return SendMessages<VoiceMessageCollection>(messages);
        }

        private MessagingResult SendMessages<TMessages>(TMessages messages)
        {
            var requestXml = Serialiser.Serialise(messages);

            RestResource resource = new MessageDispatcherResource(requestXml, EnsureMessageIdsInResult);

            return MakeRequest<MessagingResult>(HttpMethod.POST, resource);
        }
    }
}