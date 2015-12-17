using System.IO;
using System.Net;
using System.Text;
using com.esendex.sdk.adapters;
using com.esendex.sdk.http;
using NUnit.Framework;
using Moq;

namespace com.esendex.sdk.test.http
{
    [TestFixture]
    public class HttpResponseHelperTests
    {
        private HttpResponseHelper helper;

        Mock<IHttpWebResponseAdapter> mockHttpWebResponse;

        [SetUp]
        public void TestInitialize()
        {
            mockHttpWebResponse = new Mock<IHttpWebResponseAdapter>();

            helper = new HttpResponseHelper();
        }

        [Test]
        public void Create_WithNullHttpWebResponse_ReturnsNullHttpResponse()
        {
            // Arrange
            IHttpWebResponseAdapter response = null;

            // Act
            HttpResponse actualResponse = helper.Create(response);

            // Assert
            Assert.IsNull(actualResponse);
        }

        [Test]
        public void Create_WithHttpWebResponse_ReturnsHttpResponse()
        {
            // Arrange
            string expectedContent = "content";

            HttpResponse expectedResponse = new HttpResponse()
            {
                Content = expectedContent,
                ContentType = "application/xml",
                StatusCode = HttpStatusCode.OK
            };

            MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(expectedContent));

            mockHttpWebResponse.Setup(ws => ws.GetResponseStream()).Returns(stream);

            mockHttpWebResponse.SetupGet(ws => ws.StatusCode).Returns(HttpStatusCode.OK);
            mockHttpWebResponse.SetupGet(ws => ws.ContentType).Returns("application/xml");
            mockHttpWebResponse.SetupGet(ws => ws.ContentEncoding).Returns(Encoding.UTF8.WebName);

            // Act
            HttpResponse actualResponse = helper.Create(mockHttpWebResponse.Object);

            // Assert
            Assert.IsNotNull(actualResponse);
            Assert.AreEqual(expectedResponse.StatusCode, actualResponse.StatusCode);
            Assert.AreEqual(expectedResponse.ContentType, actualResponse.ContentType);
            Assert.AreEqual(expectedResponse.ContentLength, actualResponse.ContentLength);
            Assert.AreEqual(expectedResponse.Content, actualResponse.Content);
        }

        [Test]
        public void Create_WithWebException_ExceptionIsRethrown()
        {
            // Arrange
            WebException exception = new WebException();

            // Act
            try
            {
                HttpResponse actualResponse = helper.Create(exception);

                Assert.Fail();
            }
            // Assert
            catch (WebException ex)
            {
                Assert.AreEqual(exception, ex);
            }
        }
    }
}
