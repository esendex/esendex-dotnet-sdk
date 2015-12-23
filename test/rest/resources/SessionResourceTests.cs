using com.esendex.sdk.rest;
using com.esendex.sdk.rest.resources;
using NUnit.Framework;

namespace com.esendex.sdk.test.rest.resources
{
    [TestFixture]
    public class SessionResourceTests
    {
        [Test]
        public void DefaultConstructor()
        {
            // Arrange
            var expectedResourcePath = "session/constructor";

            // Act
            RestResource resource = new SessionResource();

            // Assert
            Assert.AreEqual(expectedResourcePath, resource.ResourcePath);
        }
    }
}