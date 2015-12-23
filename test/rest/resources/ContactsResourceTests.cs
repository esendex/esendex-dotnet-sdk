using System;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using NUnit.Framework;

namespace com.esendex.sdk.test.rest.resources
{
    [TestFixture]
    public class ContactsResourceTests
    {
        [Test]
        public void DefaultConstructor_WithContent()
        {
            // Arrange
            var expectedResourcePath = "contacts";
            var expectedContent = "content";

            // Act
            RestResource resource = new ContactsResource(expectedContent);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
            Assert.AreEqual(expectedContent, resource.Content);
        }

        [Test]
        public void DefaultConstructor_WithId()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedResourcePath = string.Format("contacts/{0}", id);

            // Act
            RestResource resource = new ContactsResource(id);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber1AndPageSize15()
        {
            // Arrange
            var accountReference = "frgjbhjrehre";
            var pageNumber = 1;
            var pageSize = 15;

            var expectedResourcePath = string.Format("contacts?accountReference={0}&startIndex=0&count={1}", accountReference, pageSize);

            // Act
            RestResource resource = new ContactsResource(accountReference, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber2AndPageSize15()
        {
            // Arrange
            var accountReference = "frgjbhjrehre";
            var pageNumber = 2;
            var pageSize = 15;

            var expectedResourcePath = string.Format("contacts?accountReference={0}&startIndex=15&count={1}", accountReference, pageSize);

            // Act
            RestResource resource = new ContactsResource(accountReference, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithInvalidPageNumberAndPageSize()
        {
            // Arrange
            var accountReference = "frgjbhjrehre";
            var pageNumber = 0;
            var pageSize = 0;

            // Act
            try
            {
                new ContactsResource(accountReference, pageNumber, pageSize);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentException ex)
            {
                Assert.AreEqual("pageNumber", ex.ParamName);
            }
        }

        [Test]
        public void DefaultConstructor_WithIdAndContent()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedResourcePath = string.Format("contacts/{0}", id);
            var expectedContent = "content";

            // Act
            RestResource resource = new ContactsResource(id, expectedContent);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
            Assert.AreEqual(expectedContent, resource.Content);
        }
    }
}