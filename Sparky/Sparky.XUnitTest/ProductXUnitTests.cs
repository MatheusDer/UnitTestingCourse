using Xunit;

namespace Sparky.NUnitTest;

public class ProductXUnitTests
{
    [Fact]
    public void GetPrice_ShouldReturnTheDiscount_WhenCustomerIsPlatinum()
    {
        var product = new Product(50);

        var result = product.GetPrice(new Customer() { IsPlatinum = true });

        Assert.Equal(40, result);
    }
}
