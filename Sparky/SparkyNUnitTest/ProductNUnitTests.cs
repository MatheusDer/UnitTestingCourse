using NUnit.Framework;

namespace Sparky.NUnitTest
{
    [TestFixture]
    public class ProductNUnitTests
    {
        [Test]
        public void GetPrice_ShouldReturnTheDiscount_WhenCustomerIsPlatinum()
        {
            var product = new Product(50);

            var result = product.GetPrice(new Customer() { IsPlatinum = true });

            Assert.That(result, Is.EqualTo(40));
        }
    }
}
