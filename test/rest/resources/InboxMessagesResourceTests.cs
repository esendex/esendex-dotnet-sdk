using System;
using com.esendex.sdk.inbox;
using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using NUnit.Framework;

namespace com.esendex.sdk.test.rest.resources
{
    [TestFixture]
    public class InboxMessagesResourceTests
    {
        [Test]
        public void DefaultConstructor()
        {
            // Arrange
            var expectedResourcePath = "inbox/messages";

            // Act
            RestResource resource = new InboxMessagesResource();

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber1AndPageSize()
        {
            // Arrange
            var pageNumber = 1;
            var pageSize = 15;

            var expectedResourcePath = string.Format("inbox/messages?startIndex=0&count={0}", pageSize);

            // Act
            RestResource resource = new InboxMessagesResource(pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithPageNumber2AndPageSize()
        {
            // Arrange
            var pageNumber = 2;
            var pageSize = 15;

            var expectedResourcePath = string.Format("inbox/messages?startIndex=15&count={0}", pageSize);

            // Act
            RestResource resource = new InboxMessagesResource(pageNumber, pageSize);

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
                RestResource resource = new InboxMessagesResource(pageNumber, pageSize);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentException ex)
            {
                Assert.AreEqual("pageNumber", ex.ParamName);
            }
        }

        [Test]
        public void DefaultConstructor_WithAccountReference()
        {
            // Arrange
            var accountReference = "accountReference";

            var expectedResourcePath = string.Format("inbox/{0}/messages", accountReference);

            // Act
            RestResource resource = new InboxMessagesResource(accountReference);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithNullAccountReference()
        {
            // Arrange
            string accountReference = null;

            // Act
            try
            {
                RestResource resource = new InboxMessagesResource(accountReference);

                Assert.Fail();
            }
                // Assert
            catch (ArgumentException ex)
            {
                Assert.AreEqual("accountReference", ex.ParamName);
            }
        }

        [Test]
        public void DefaultConstructor_WithAccountReferenceAndPageNumberAndPageSize()
        {
            // Arrange
            var accountReference = "accountReference";
            var pageNumber = 2;
            var pageSize = 15;

            var expectedResourcePath = string.Format("inbox/{0}/messages?startIndex=15&count={1}", accountReference, pageSize);

            // Act
            RestResource resource = new InboxMessagesResource(accountReference, pageNumber, pageSize);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithIdAndInboxMessageStatus()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedResourcePath = string.Format("inbox/messages/{0}?action=read", id);

            // Act
            RestResource resource = new InboxMessagesResource(id, InboxMessageStatus.Read);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithId()
        {
            // Arrange
            var id = Guid.NewGuid();

            var expectedResourcePath = string.Format("inbox/messages/{0}", id);

            // Act
            RestResource resource = new InboxMessagesResource(id);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithDateRange()
        {
            // Arrange
            DateTime start = DateTime.Today;
            DateTime finish = DateTime.Today.AddDays(1);
            var expectedResourcePath = string.Format("inbox/messages?start={0}Z&finish={1}", start.ToString("yyyy-MM-ddTHH:mm:ss"), finish.ToString("yyyy-MM-ddTHH:mm:ss"));

            // Act
            RestResource resource = new InboxMessagesResource(start, finish);

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }
    }
}