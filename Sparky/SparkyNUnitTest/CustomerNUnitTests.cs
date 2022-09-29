using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class CustomerNUnitTests
{
    private Customer customer;

    [SetUp]
    public void Setup()
    {
        customer = new();
    }

    [Test]
    public void CombineNames_TwoNames_ReturnFullName()
    {
        var result = customer.CombineNames("Ab", "Cd");

        Assert.Multiple(() =>
        {
            Assert.AreEqual("Ab Cd", result);
            Assert.That(result, Does.Contain(" "));
            Assert.That(result, Does.StartWith("a").IgnoreCase);
            Assert.That(result, Does.Match("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
        });
    }

    [Test]
    public void GreetMessage_NotGreeted_ReturnsNull()
    {
        Assert.IsNull(customer.GreetMessage);
    }

    [Test]
    public void GreetMessage_Greeted_ReturnsAMessage()
    {
        customer.GreetAndCombineNames("name", "lastName");

        Assert.IsNotNull(customer.GreetMessage);
    }

    [Test]
    public void DiscountCheck_DefaultCustomer_ReturnsDiscountInRange()
    {
        Assert.That(customer.Discount, Is.InRange(10, 25));
    }

    [Test]
    public void GreetMessage_GreetedWithoutLastName_ReturnsMessage()
    {
        customer.GreetAndCombineNames("name", "");

        Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
    }

    [Test]
    public void GreetMessage_GreetedWithoutFirstName_ShouldThrow()
    {
        //var exceptionDetails = Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", ""));
        //Assert.AreEqual("Empty first name", exceptionDetails);

        Assert.That(() => customer.GreetAndCombineNames("", ""), 
            Throws.ArgumentException.With.Message.EqualTo("Empty first name"));
    }
}
