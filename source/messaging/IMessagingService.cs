using System;

namespace com.esendex.sdk.messaging
{
    /// <summary>
    /// Defines methods to send messages
    /// </summary>
    public interface IMessagingService
    {
        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessage instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.SmsMessage instance that contains the SMS message.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendMessage(SmsMessage message);

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessage instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.VoiceMessage instance that contains the Voice message.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendMessage(VoiceMessage message);

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessageCollection instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.SmsMessageCollection instance that contains an SMS message collection.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendMessages(SmsMessageCollection messages);

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessageCollection instance and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.VoiceMessageCollection instance that contains a Voice message collection.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendMessages(VoiceMessageCollection messages);

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessage instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.SmsMessage instance that contains a SMS message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the message should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendScheduledMessage(SmsMessage message, DateTime sendAt);

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessage instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="message">A com.esendex.sdk.messaging.VoiceMessage instance that contains a Voice message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the message should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendScheduledMessage(VoiceMessage message, DateTime sendAt);

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.SmsMessageCollection instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.SmsMessageCollection instance that contains a SMS message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the messages should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendScheduledMessages(SmsMessageCollection messages, DateTime sendAt);

        /// <summary>
        /// Sends a com.esendex.sdk.messaging.VoiceMessageCollection instance scheduled at a System.DateTime and returns a com.esendex.sdk.messaging.MessagingResult instance.
        /// </summary>
        /// <param name="messages">A com.esendex.sdk.messaging.VoiceMessageCollection instance that contains a Voice message collection.</param>
        /// <param name="sendAt">A System.DateTime instance that contains the date and time at which the messages should be sent.</param>
        /// <returns>A com.esendex.sdk.messaging.MessagingResult instance that contains the message batch Id.</returns>
        /// <exception cref="System.Net.WebException"></exception>
        MessagingResult SendScheduledMessages(VoiceMessageCollection messages, DateTime sendAt);
    }
}