using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class CustomerNUnitTests
{
    [Test]
    public void CombineNames_TwoNames_ReturnFullName()
    {
        var customer = new Customer();

        var result = customer.CombineNames("Ab", "Cd");

        Assert.AreEqual("Ab Cd", result);
        Assert.That(result, Does.Contain(" "));
        Assert.That(result, Does.StartWith("a").IgnoreCase);
        Assert.That(result, Does.Match("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
    }

    [Test]
    public void GreetMessage_NotGreeted_ReturnsNull()
    {
        var customer = new Customer();

        Assert.IsNull(customer.GreetMessage);
    }

    [Test]
    public void GreetMessage_Greeted_ReturnsAMessage()
    {
        var customer = new Customer();

        customer.GreetAndCombineNames("name", "lastName");

        Assert.IsNotNull(customer.GreetMessage);
    }
}
