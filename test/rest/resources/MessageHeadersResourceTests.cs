using System;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using NUnit.Framework;

namespace com.esendex.sdk.test.rest.resources
{
    [TestFixture]
    public class MessageHeadersResourceTests
    {
        [Test]
        public void DefaultConstructor_WithId()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string expectedResourcePath = string.Format("messageheaders/{0}", id);

            // Act
            RestResource resource = new MessageHeadersResource(id);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber1AndPageSize()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 15;

            string expectedResourcePath = string.Format("messageheaders?startIndex=0&count={0}", pageSize);

            // Act
            RestResource resource = new MessageHeadersResource(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber2AndPageSize()
        {
            // Arrange
            int pageNumber = 2;
            int pageSize = 15;

            string expectedResourcePath = string.Format("messageheaders?startIndex=15&count={0}", pageSize);

            // Act
            RestResource resource = new MessageHeadersResource(pageNumber, pageSize);

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
                RestResource resource = new MessageHeadersResource(pageNumber, pageSize);

                Assert.Fail();
            }
            // Assert
            catch (ArgumentException ex)
            {
                Assert.AreEqual("pageNumber", ex.ParamName);
            }
        }

        [Test]
        public void DefaultConstructor_WithPageNumberAndPageSizeAndAccountReference()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 15;
            string accountReference = "accountReference";

            string expectedResourcePath =
                string.Format("messageheaders?startIndex=0&count={0}&filterBy=account&filterValue={1}",
                    pageSize, accountReference);

            // Act
            RestResource resource = new MessageHeadersResource(accountReference, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithNullAccountReference()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 15;
            string accountReference = null;

            // Act
            try
            {
                RestResource resource = new MessageHeadersResource(accountReference, pageNumber, pageSize);

                Assert.Fail();
            }
            // Assert
            catch (ArgumentException ex)
            {
                Assert.AreEqual("accountReference", ex.ParamName);
            }
        }
    }
}
