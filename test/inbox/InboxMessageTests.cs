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
            InboxMessageCollection collection = new InboxMessageCollection();

            //  Assert
            Assert.That(collection.Messages, Is.InstanceOf<IEnumerable<InboxMessage>>());
        }

        [Test]
        public void InboxMessageCollection_SetPageNumber_WithZeroIndex_GetPageNumber_ReturnsPageNumber1()
        {
            // Arrange
            int startIndex = 0;
            int pageSize = 15;

            int expectedPageNumber = 1;

            InboxMessageCollection collection = new InboxMessageCollection()
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
            int startIndex = 30;
            int pageSize = 15;

            int expectedPageNumber = 3;

            InboxMessageCollection collection = new InboxMessageCollection()
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