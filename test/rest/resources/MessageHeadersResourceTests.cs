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
            var id = Guid.NewGuid();
            var expectedResourcePath = string.Format("messageheaders/{0}", id);

            // Act
            RestResource resource = new MessageHeadersResource(id);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber1AndPageSize()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 15;

            var expectedResourcePath = string.Format("messageheaders?startIndex=0&count={0}", pageSize);

            // Act
            RestResource resource = new MessageHeadersResource(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber2AndPageSize()
        {
            // Arrange
            var pageNumber = 2;
            var pageSize = 15;

            var expectedResourcePath = string.Format("messageheaders?startIndex=15&count={0}", pageSize);

            // Act
            RestResource resource = new MessageHeadersResource(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithInvalidPageNumberAndPageSize()
        {
            // Arrange
            var pageNumber = 0;
            var pageSize = 0;

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
            var pageNumber = 1;
            var pageSize = 15;
            var accountReference = "accountReference";

            var expectedResourcePath =
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
            var pageNumber = 1;
            var pageSize = 15;
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