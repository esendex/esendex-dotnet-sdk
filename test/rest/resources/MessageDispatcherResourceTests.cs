using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using NUnit.Framework;

namespace com.esendex.sdk.test.rest.resources
{
    [TestFixture]
    public class MessageDispatcherResourceTests
    {
        [Test]
        public void DefaultConstructor_WithMessageIdsInResult()
        {
            // Arrange
            string content = "content";
            bool ensureMessageIdsInResult = true;

            // Act
            RestResource resource = new MessageDispatcherResource(content, ensureMessageIdsInResult);

            // Assert
            Assert.AreEqual(content, resource.Content);
            Assert.AreEqual("messagedispatcher?returnmessageheaders=true", resource.ResourcePath);
        }

        [Test]
        public void DefaultConstructor_WithoutMessageIdsInResult()
        {
            // Arrange
            string content = "content";
            bool ensureMessageIdsInResult = false;

            // Act
            RestResource resource = new MessageDispatcherResource(content, ensureMessageIdsInResult);

            // Assert
            Assert.AreEqual(content, resource.Content);
            Assert.AreEqual("messagedispatcher", resource.ResourcePath);
        }
    }
}
