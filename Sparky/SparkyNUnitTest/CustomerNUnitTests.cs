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
}
