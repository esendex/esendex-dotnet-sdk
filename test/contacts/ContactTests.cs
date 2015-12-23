using System;
using System.Collections.Generic;
using System.Linq;
using com.esendex.sdk.contacts;
using NUnit.Framework;

namespace com.esendex.sdk.test.contacts
{
    [TestFixture]
    public class ContactTests
    {
        [Test]
        public void Contact_DefaultDIConstructor()
        {
            // Arrange
            const string accountReference = "account";
            const string quickname = "quickname";
            const string phonenumber = "phonenumber";

            // Act
            var contactInstance = new Contact(accountReference, quickname, phonenumber);

            // Assert
            Assert.AreEqual(accountReference, contactInstance.AccountReference);
            Assert.AreEqual(quickname, contactInstance.QuickName);
            Assert.AreEqual(phonenumber, contactInstance.PhoneNumber);
        }

        [Test]
        public void Contact_DefaultDIConstructor_WithNullQuickNameAndMobileNumber_ThrowsException()
        {
            // Arrange
            const string accountReference = "account";
            var quickname = string.Empty;
            string phonenumber = null;

            // Act
            try
            {
                new Contact(accountReference, quickname, phonenumber);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("quickName", ex.ParamName);
            }
        }

        [Test]
        public void ContactCollection_DefaultConstructor()
        {
            // Arrange

            // Act
            var contactCollectionInstance = new ContactCollection();

            // Assert
            Assert.That(contactCollectionInstance.Items, Is.InstanceOf<List<Contact>>());
        }

        [Test]
        public void ContactCollection_DefaultDIConstructor_WithContact()
        {
            // Arrange
            var contact = new Contact();

            // Act
            var contactCollectionInstance = new ContactCollection(contact);

            // Assert
            Assert.AreEqual(contact, contactCollectionInstance.Items.ElementAt(0));
        }

        [Test]
        public void ContactCollection_DefaultDIConstructor_WithNullContact_ThrowsArgumentNullException()
        {
            // Arrange
            Contact contact = null;

            // Act
            try
            {
                var contactCollectionInstance = new ContactCollection(contact);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("contact", ex.ParamName);
            }
        }

        [Test]
        public void ContactCollection_DefaultDIConstructor_WithContactsArray()
        {
            // Arrange
            var contact = new Contact();

            var contacts = new List<Contact>();

            contacts.Add(contact);

            // Act
            var contactCollectionInstance = new ContactCollection(contacts);

            // Assert
            Assert.AreEqual(contact, contactCollectionInstance.Items.ElementAt(0));
        }

        [Test]
        public void ContactCollection_DefaultDIConstructor_WithNullContactArray_ThrowsArgumentNullException()
        {
            // Arrange
            List<Contact> contacts = null;

            // Act
            try
            {
                var contactCollectionInstance = new ContactCollection(contacts);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("contacts", ex.ParamName);
            }
        }
    }
}