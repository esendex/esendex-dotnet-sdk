using System;
using System.Net;
using com.esendex.sdk.contacts;
using com.esendex.sdk.core;
using com.esendex.sdk.inbox;
using com.esendex.sdk.messaging;
using com.esendex.sdk.sent;
using com.esendex.sdk.session;

namespace com.esendex.sdk.samples
{
    class Program
    {
        static EsendexCredentials credentials;
        static EsendexCredentials Credentials
        {
            get
            {
                // TODO: Add your username, password and proxy information here as required.
                return credentials ?? (credentials = new EsendexCredentials("username", "password"));
            }
        }

        static string accountReference
        {
            // TODO: Add your account reference here.
            get { return "EX000000"; }
        }

        static void Main(string[] args)
        {
            // Use session authentication
            try
            {
                SessionService sessionService = new SessionService(Credentials);

                Credentials.SessionId = sessionService.CreateSession();
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
            
            // TODO: Remove these comments to expose the example functionality.

            // SendMessageExample();

            // GetSentMessagesExample();

            // GetInboxMessagesExample();

            // GetContactsExample();

            Console.ReadLine();
        }

        private static void GetContactsExample()
        {
            int pageNumber = 1;
            int pageSize = 15;

            ContactService contactService = new ContactService(Credentials);

            try
            {
                PagedContactCollection collection = contactService.GetContacts(pageNumber, pageSize);

                foreach (Contact item in collection.Contacts)
                {
                    Console.WriteLine("Contact Id:{0}\nQuickname:{1}\n\n", item.Id, item.QuickName);
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void GetSentMessagesExample()
        {
            int pageNumber = 1;
            int pageSize = 15;

            SentService sentService = new SentService(Credentials);

            try
            {
                SentMessageCollection collection = sentService.GetMessages(pageNumber, pageSize);

                foreach (SentMessage item in collection.Messages)
                {
                    Console.WriteLine("Message Id:{0}\nMessage:{1}\n\n", item.Id, item.Summary);
                }

            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void SendMessageExample()
        {
            SmsMessage message = new SmsMessage("07000000000", "This is a test...", accountReference);

            MessagingService messagingService = new MessagingService(true, Credentials);

            try
            {
                MessagingResult messageResult = messagingService.SendMessage(message);

                Console.WriteLine("Message Batch Id: {0}", messageResult.BatchId);

                foreach (ResourceLink messageId in messageResult.MessageIds)
                {
                    Console.WriteLine("Message Uri: {0}", messageId.Uri);
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void GetInboxMessagesExample()
        {
            int pageNumber = 1;
            int pageSize = 15;

            InboxService inboxService = new InboxService(Credentials);

            try
            {
                InboxMessageCollection collection = inboxService.GetMessages(pageNumber, pageSize);

                foreach (InboxMessage item in collection.Messages)
                {
                    Console.WriteLine("Message Id:{0}\nMessage:{1}\n\n", item.Id, item.Summary);
                }

            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}