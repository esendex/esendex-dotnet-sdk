using com.esendex.sdk.test.mockapi;
using NUnit.Framework;

namespace com.esendex.sdk.test
{
    [SetUpFixture]
    public class SetUpFixture
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            MockApi.Start("localhost:6789");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            MockApi.Stop();
        }
    }
}
