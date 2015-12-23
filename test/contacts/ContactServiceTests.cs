using System;
using System.Net;
using com.esendex.sdk.contacts;
using com.esendex.sdk.http;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using com.esendex.sdk.utilities;
using Moq;
using NUnit.Framework;

namespace com.esendex.sdk.test.contacts
{
    [TestFixture]
    public class ContactServiceTests
    {
        private ContactService service;

        private Mock<ISerialiser> mockSerialiser;
        private Mock<IRestClient> mockRestClient;

        [SetUp]
        public void TestInitialize()
        {
            mockSerialiser = new Mock<ISerialiser>();
            mockRestClient = new Mock<IRestClient>();

            service = new ContactService(mockRestClient.Object, mockSerialiser.Object);
        }

        [Test]
        public void DefaultConstructor()
        {
            // Arrange
            var credentials = new EsendexCredentials("username", "password");

            // Act
            var serviceInstance = new ContactService(credentials);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());
        }

        [Test]
        public void DefaultDIConstructor()
        {
            // Arrange
            var uri = new Uri("http://tempuri.org");
            var credentials = new EsendexCredentials("username", "password");
            IHttpRequestHelper httpRequestHelper = new HttpRequestHelper();
            IHttpResponseHelper httpResponseHelper = new HttpResponseHelper();
            IHttpClient httpClient = new HttpClient(credentials, uri, httpRequestHelper, httpResponseHelper);

            IRestClient restClient = new RestClient(httpClient);
            ISerialiser serialiser = new XmlSerialiser();

            // Act
            var serviceInstance = new ContactService(restClient, serialiser);

            // Assert
            Assert.That(serviceInstance.RestClient, Is.InstanceOf<RestClient>());
            Assert.That(serviceInstance.Serialiser, Is.InstanceOf<XmlSerialiser>());
        }

        [Test]
        public void CreateContact_WithContact_ReturnsContactWithId()
        {
            // Arrange
            var requestedContact = new Contact();

            var serialisedContent = "serialisedContent";

            RestResource resource = new ContactsResource(serialisedContent);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            var expectedContact = new Contact();

            mockSerialiser
                .Setup(s => s.Serialise(requestedContact))
                .Returns(serialisedContent);

            mockRestClient
                .Setup(r => r.Post(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<ContactResponse>(response.Content))
                .Returns(new ContactResponse {Contact = expectedContact});

            // Act
            var actualContact = service.CreateContact(requestedContact);

            // Assert
            Assert.AreEqual(expectedContact, actualContact);
        }

        [Test]
        public void UpdateContact_WithContact_ReturnsTrueWhenSuccessful()
        {
            // Arrange
            var requestedContact = new Contact
            {
                Id = Guid.NewGuid()
            };

            var serialisedContent = "serialisedContent";

            RestResource resource = new ContactsResource(requestedContact.Id, serialisedContent);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            mockSerialiser
                .Setup(s => s.Serialise(requestedContact))
                .Returns(serialisedContent);

            mockRestClient
                .Setup(r => r.Put(resource))
                .Returns(response);

            // Act
            var actualResult = service.UpdateContact(requestedContact);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void UpdateContact_WithContact_ReturnsFalseWhenFailed()
        {
            // Arrange
            var requestedContact = new Contact
            {
                Id = Guid.NewGuid()
            };

            var serialisedContent = "serialisedContent";

            RestResource resource = new ContactsResource(requestedContact.Id, serialisedContent);

            RestResponse response = null;

            mockSerialiser
                .Setup(s => s.Serialise(requestedContact))
                .Returns(serialisedContent);

            mockRestClient
                .Setup(r => r.Put(resource))
                .Returns(response);

            // Act
            var actualResult = service.UpdateContact(requestedContact);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void DeleteContact_WithId_ReturnsTrueWhenSuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();

            RestResource resource = new ContactsResource(id);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            mockRestClient
                .Setup(r => r.Delete(resource))
                .Returns(response);

            // Act
            var actualResult = service.DeleteContact(id);

            // Assert
            Assert.IsTrue(actualResult);
        }

        [Test]
        public void DeleteContact_WithId_ReturnsFalseWhenUnsuccessful()
        {
            // Arrange
            var id = Guid.NewGuid();

            RestResource resource = new ContactsResource(id);

            RestResponse response = null;

            mockRestClient
                .Setup(r => r.Delete(resource))
                .Returns(response);

            // Act
            var actualResult = service.DeleteContact(id);

            // Assert
            Assert.IsFalse(actualResult);
        }

        [Test]
        public void GetContact_WithId_ReturnsContact()
        {
            // Arrange
            var id = Guid.NewGuid();

            RestResource resource = new ContactsResource(id);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK
            };

            var expectedContact = new Contact();

            mockRestClient
                .Setup(r => r.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<Contact>(response.Content))
                .Returns(expectedContact);

            // Act
            var actualContact = service.GetContact(id);

            // Assert
            Assert.AreEqual(expectedContact, actualContact);
        }

        [Test]
        public void GetContacts_WithPageNumberWithPageSize_ReturnsContacts()
        {
            // Arrange            
            var accountReference = "frgjbhjrehre";
            var pageNumber = 1;
            var pageSize = 15;

            RestResource resource = new ContactsResource(accountReference, pageNumber, pageSize);

            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "content"
            };

            var expectedContacts = new PagedContactCollection
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            mockRestClient
                .Setup(r => r.Get(resource))
                .Returns(response);

            mockSerialiser
                .Setup(s => s.Deserialise<PagedContactCollection>(response.Content))
                .Returns(expectedContacts);

            // Act
            var actualContact = service.GetContacts(accountReference, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(pageNumber, actualContact.PageNumber);
            Assert.AreEqual(pageSize, actualContact.PageSize);
        }
    }
}