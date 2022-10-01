using Xunit;

namespace Sparky.NUnitTest;

public class CustomerXUnitTests
{
    private Customer customer;

    public CustomerXUnitTests()
    {
        customer = new();
    }

    [Fact]
    public void CombineNames_TwoNames_ReturnFullName()
    {
        var result = customer.CombineNames("Ab", "Cd");

        Assert.Multiple(() =>
        {
            Assert.Equal("Ab Cd", result);
            Assert.Contains(" ", result);
            Assert.StartsWith("a", result, StringComparison.CurrentCultureIgnoreCase);
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", result);
        });
    }

    [Fact]
    public void GreetMessage_NotGreeted_ReturnsNull()
    {
        Assert.Null(customer.GreetMessage);
    }

    [Fact]
    public void GreetMessage_Greeted_ReturnsAMessage()
    {
        customer.GreetAndCombineNames("name", "lastName");

        Assert.NotNull(customer.GreetMessage);
    }

    [Fact]
    public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
    {
        Assert.InRange(customer.Discount, 10, 25);
    }

    [Fact]
    public void GreetMessage_GreetedWithoutLastName_ReturnsMessage()
    {
        customer.GreetAndCombineNames("name", "");

        Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
    }

    [Fact]
    public void GreetMessage_GreetedWithoutFirstName_ShouldThrow()
    {
        var exception = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", ""));
        Assert.Equal("Empty first name", exception.Message);
    }

    [Fact]
    public void CustomerType_CreateCustomerWithLessThan100Order_ReturnsBasicCustomer()
    {
        customer.OrderTotal = 10;
        var result = customer.GetCustomerDetail();

        Assert.IsType<BasicCustomer>(result);
    }

    [Fact]
    public void CustomerType_CreateCustomerWithGreaterThan100Order_ReturnsPlatinumCustomer()
    {
        customer.OrderTotal = 101;
        var result = customer.GetCustomerDetail();

        Assert.IsType<PlatinumCustomer>(result);
    }
}
