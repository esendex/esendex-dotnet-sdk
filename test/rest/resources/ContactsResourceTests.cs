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
            string expectedResourcePath = "contacts";
            string expectedContent = "content";

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
            Guid id = Guid.NewGuid();

            string expectedResourcePath = string.Format("contacts/{0}", id);

            // Act
            RestResource resource = new ContactsResource(id);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber1AndPageSize15()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            int pageNumber = 1;
            int pageSize = 15;

            string expectedResourcePath = string.Format("contacts?startIndex=0&count={0}", pageSize);

            // Act
            RestResource resource = new ContactsResource(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber2AndPageSize15()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            int pageNumber = 2;
            int pageSize = 15;

            string expectedResourcePath = string.Format("contacts?startIndex=15&count={0}", pageSize);            

            // Act
            RestResource resource = new ContactsResource(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithInvalidPageNumberAndPageSize()
        {
            // Arrange
            int pageNumber = 0;
            int pageSize = 0;

            // Act
            try
            {
                RestResource resource = new ContactsResource(pageNumber, pageSize);

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
            Guid id = Guid.NewGuid();

            string expectedResourcePath = string.Format("contacts/{0}", id);
            string expectedContent = "content";            

            // Act
            RestResource resource = new ContactsResource(id, expectedContent);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
            Assert.AreEqual(expectedContent, resource.Content);
        }
    }
}
