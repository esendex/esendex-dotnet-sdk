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
            string text =  "this is a test";

            int expectedContentLength = Encoding.UTF8.GetByteCount(text);

            HttpRequest request = new HttpRequest()
            {
                Content = text     
            };

            // Act
            long actualContentLength = request.ContentLength;

            // Assert
            Assert.AreEqual(expectedContentLength, actualContentLength);
        }

        [Test]
        public void ContentLength_WithNonASCIICharacters()
        {
            // Arrange
            string text = "this is a £££ test";

            int expectedContentLength = Encoding.UTF8.GetByteCount(text);

            HttpRequest httpRequest = new HttpRequest()
            {
                Content = text
            };

            // Act
            long actualContentLength = httpRequest.ContentLength;

            // Assert
            Assert.AreEqual(expectedContentLength, actualContentLength);
        }

        [Test]
        public void DefaultConstructor_SetsEncodingType()
        {
            // Arrange

            // Act
            HttpRequest httpRequest = new HttpRequest();

            // Assert
            Assert.AreEqual(Encoding.UTF8, httpRequest.ContentEncoding);
        }
    }
}