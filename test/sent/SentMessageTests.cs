using System.Collections.Generic;
using com.esendex.sdk.sent;
using NUnit.Framework;

namespace com.esendex.sdk.test.sent
{
    [TestFixture]
    public class SentMessageTests
    {
        [Test]
        public void SentMessageCollection_DefaultDIConstructor()
        {
            //  Arrange            

            //  Act
            var collection = new SentMessageCollection();

            //  Assert
            Assert.That(collection.Messages, Is.InstanceOf<IEnumerable<SentMessage>>());
        }

        [Test]
        public void SentMessageCollection_SetPageNumber_WithZeroIndex_GetPageNumber_ReturnsPageNumber1()
        {
            // Arrange
            var startIndex = 0;
            var pageSize = 15;

            var expectedPageNumber = 1;

            var collection = new SentMessageCollection
            {
                PageSize = pageSize
            };

            // Act
            collection.PageNumber = startIndex;

            // Assert
            Assert.AreEqual(expectedPageNumber, collection.PageNumber);
        }

        [Test]
        public void SentMessageCollection_SetPageNumber_WithNonZeroIndex_GetPageNumber_ReturnsPageNumber()
        {
            // Arrange
            var startIndex = 30;
            var pageSize = 15;

            var expectedPageNumber = 3;

            var collection = new SentMessageCollection
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