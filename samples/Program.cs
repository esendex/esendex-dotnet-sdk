using System;
using System.Linq;
using System.Net;
using com.esendex.sdk.contacts;
using com.esendex.sdk.groups;
using com.esendex.sdk.inbox;
using com.esendex.sdk.messaging;
using com.esendex.sdk.sent;
using com.esendex.sdk.session;
using Mono.Options;

namespace com.esendex.sdk.samples
{
    class Program
    {
        private const int PageIndex = 1;
        private const int PageSize = 15;
        private static string _username;
        private static string _password;
        private static string _accountReference;
        private static string _sendTo;
        private static bool _getBodies;

        static void Main(string[] args)
        {
            var helpRequested = false;
            var sendMessage = false;
            var optionSet = new OptionSet
            {
                {"u|user=", "Username to use", user => _username = user},
                {"p|pass=", "Password for Username", pass => _password = pass},
                {"a|account=", "Account Reference to use", reference => _accountReference = reference},
                {
                    "s|send=", "Send a message to the provided number", sendTo =>
                    {
                        sendMessage = true;
                        _sendTo = sendTo;
                    }
                },
                {"b|bodies+", "Retrieve message bodies", v => _getBodies = true},
                {
                    "h|help", "Help about the command line interface", key => { helpRequested = key != null; }
                }
            };

            try
            {
                optionSet.Parse(args);
                if (!helpRequested && (string.IsNullOrEmpty(_username) ||
                                       string.IsNullOrEmpty(_password) ||
                                       string.IsNullOrEmpty(_accountReference)))
                    throw new ApplicationException("Samples require username, password and account reference be given");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                helpRequested = true;
            }

            if (helpRequested)
            {
                ShowUsage(optionSet);
                return;
            }

            EsendexCredentials credentials;
            try
            {
                credentials = new EsendexCredentials(_username, _password);
                var sessionService = new SessionService(credentials);

                credentials.SessionId = sessionService.CreateSession();
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
                return;
            }

            if (sendMessage)
            {
                Console.WriteLine("Send Message Example\r\n");
                SendMessageExample(credentials);
            }

            MessageBodyService messageBodyService = null;
            if (_getBodies)
            {
                messageBodyService = new MessageBodyService(credentials);
            }

            Console.WriteLine();
            Console.WriteLine("Sent Messages Example\r\n");
            GetSentMessagesExample(credentials, messageBodyService);

            Console.WriteLine();
            Console.WriteLine("Inbox Messages Example\r\n");
            GetInboxMessagesExample(credentials, messageBodyService);

            Console.WriteLine();
            Console.WriteLine("Contacts Example\r\n");
            GetContactsExample(credentials);

            Console.WriteLine();
            Console.WriteLine("Groups Example\r\n");
            GetGroupsExample(credentials);

            Console.WriteLine();
            Console.WriteLine("Contacts in Group Example\r\n");
            GetContactsByGroupExample(credentials);

            AddContacttoGroup(credentials);

            Console.WriteLine();
            Console.WriteLine("Press enter to continue ... ");
            Console.ReadLine();
        }

        private static void ShowUsage(OptionSet optionSet)
        {
            Console.WriteLine(@"Esendex .Net SDK Samples");

            optionSet.WriteOptionDescriptions(Console.Out);

            Console.WriteLine(@"Enjoy...");
        }

        private static void SendMessageExample(EsendexCredentials credentials)
        {
            var message = new SmsMessage(_sendTo, "This is a test message from the .Net SDK...", _accountReference);

            var messagingService = new MessagingService(true, credentials);

            try
            {
                var messageResult = messagingService.SendMessage(message);

                Console.WriteLine("\tMessage Batch Id: {0}", messageResult.BatchId);

                foreach (var messageId in messageResult.MessageIds)
                {
                    Console.WriteLine("\t\tMessage Uri: {0}", messageId.Uri);
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void GetSentMessagesExample(EsendexCredentials credentials, MessageBodyService messageBodyService)
        {
            var sentService = new SentService(credentials);

            try
            {
                var collection = sentService.GetMessages(_accountReference, PageIndex, PageSize);

                foreach (var item in collection.Messages)
                {
                    if (messageBodyService != null)
                    {
                        messageBodyService.LoadBodyText(item.Body);
                        Console.WriteLine("\tMessage Id:{0}\tSummary:{1}\n\tBody:{2}\n",
                                          item.Id,
                                          item.Summary,
                                          item.Body.BodyText);
                    }
                    else
                    {
                        Console.WriteLine("\tMessage Id:{0}\tSummary:{1}",
                                          item.Id,
                                          item.Summary);
                    }
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void GetInboxMessagesExample(EsendexCredentials credentials, MessageBodyService messageBodyService)
        {
            var inboxService = new InboxService(credentials);

            try
            {
                var collection = inboxService.GetMessages(_accountReference, PageIndex, PageSize);

                foreach (var item in collection.Messages)
                {
                    if (messageBodyService != null)
                    {
                        messageBodyService.LoadBodyText(item.Body);
                        Console.WriteLine("\tMessage Id:{0}\tSummary:{1}\n\tBody:{2}\n",
                                          item.Id,
                                          item.Summary,
                                          item.Body.BodyText);
                    }
                    else
                    {
                        Console.WriteLine("\tMessage Id:{0}\tSummary:{1}",
                                          item.Id,
                                          item.Summary);
                    }
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void GetContactsExample(EsendexCredentials credentials)
        {
            var contactService = new ContactService(credentials);

            try
            {
                var collection = contactService.GetContacts(_accountReference, PageIndex, PageSize);

                foreach (var item in collection.Contacts)
                {
                    Console.WriteLine("\tContact Id:{0}\tQuickname:{1}", item.Id, item.QuickName);
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void GetGroupsExample(EsendexCredentials credentials)
        {
            var groupService = new GroupService(credentials);

            try
            {
                var collection = groupService.GetGroups(_accountReference, PageIndex, PageSize);

                foreach (var item in collection.Groups)
                {
                    Console.WriteLine("\tGroup Id:{0}\tName:{1}", item.Id, item.Name);
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void GetContactsByGroupExample(EsendexCredentials credentials)
        {
            var groupService = new GroupService(credentials);

            try
            {
                var collection = groupService.GetGroups(_accountReference, PageIndex, PageSize);
                var contacts = new PagedContactCollection();

                var groupId = "";

                foreach (var item in collection.Groups.Where(item => item.Name == "Test group"))
                {
                    groupId = item.Id.ToString();
                    break;
                }

                if (groupId == "") return;

                contacts = groupService.GetContactsFromGroup(_accountReference, groupId, 1, 15);

                foreach (var item in contacts.Contacts)
                {
                    Console.WriteLine("\tContact Id:{0}\tNumber:{1}", item.Id, item.PhoneNumber);
                }
            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private static void AddContacttoGroup(EsendexCredentials credentials)
        {
            var groupService = new GroupService(credentials);
            var contactService = new ContactService(credentials);

            try
            {
                var guid = new Guid("6c5e0669-af2e-4682-85c1-bd97a45c590d");
                var contact = contactService.GetContact(guid);
                groupService.AddContactToGroup(_accountReference, "1c259623-00bf-4629-af38-b1f770b12634", contact);

            }
            catch (WebException ex)
            {
                Console.Write(ex.Message);
            }
        }
    }
}