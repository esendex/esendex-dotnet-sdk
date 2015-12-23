using System.Text;
using com.esendex.sdk.http;
using NUnit.Framework;

namespace com.esendex.sdk.test.http
{
    [TestFixture]
    public class HttpRequestTests
    {
        [Test]
        public void ContentLength_WithStandardCharacters()
        {
            // Arrange
            var text = "this is a test";

            var expectedContentLength = Encoding.UTF8.GetByteCount(text);

            var request = new HttpRequest
            {
                Content = text
            };

            // Act
            var actualContentLength = request.ContentLength;

            // Assert
            Assert.AreEqual(expectedContentLength, actualContentLength);
        }

        [Test]
        public void ContentLength_WithNonASCIICharacters()
        {
            // Arrange
            var text = "this is a £££ test";

            var expectedContentLength = Encoding.UTF8.GetByteCount(text);

            var httpRequest = new HttpRequest
            {
                Content = text
            };

            // Act
            var actualContentLength = httpRequest.ContentLength;

            // Assert
            Assert.AreEqual(expectedContentLength, actualContentLength);
        }

        [Test]
        public void DefaultConstructor_SetsEncodingType()
        {
            // Arrange

            // Act
            var httpRequest = new HttpRequest();

            // Assert
            Assert.AreEqual(Encoding.UTF8, httpRequest.ContentEncoding);
        }
    }
}