using System.Collections.Generic;
using com.esendex.sdk.inbox;
using NUnit.Framework;

namespace com.esendex.sdk.test.inbox
{
    [TestFixture]
    public class InboxMessagesTests
    {
        [Test]
        public void InboxMessageCollection_DefaultDIConstructor()
        {
            //  Arrange            

            //  Act
            var collection = new InboxMessageCollection();

            //  Assert
            Assert.That(collection.Messages, Is.InstanceOf<IEnumerable<InboxMessage>>());
        }

        [Test]
        public void InboxMessageCollection_SetPageNumber_WithZeroIndex_GetPageNumber_ReturnsPageNumber1()
        {
            // Arrange
            var startIndex = 0;
            var pageSize = 15;

            var expectedPageNumber = 1;

            var collection = new InboxMessageCollection
            {
                PageSize = pageSize
            };

            // Act
            collection.PageNumber = startIndex;

            // Assert
            Assert.AreEqual(expectedPageNumber, collection.PageNumber);
        }

        [Test]
        public void InboxMessageCollection_SetPageNumber_WithNonZeroIndex_GetPageNumber_ReturnsPageNumber()
        {
            // Arrange
            var startIndex = 30;
            var pageSize = 15;

            var expectedPageNumber = 3;

            var collection = new InboxMessageCollection
            {
                PageSize = pageSize
            };

            // Act
            collection.PageNumber = startIndex;

            // Assert
            Assert.AreEqual(expectedPageNumber, collection.PageNumber);
        }
    }
}