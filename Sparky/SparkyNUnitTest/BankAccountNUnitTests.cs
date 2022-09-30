using Moq;
using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class BankAccountNUnitTests
{
    private BankAccount bankAccount;

    [SetUp]
    protected void Setup()
    {
    }

    [Test]
    public void DepositLogFakker_ShouldReturnTrue_WhenAdd100()
    {
        var bankAccount = new BankAccount(new FakkerLogBook()); //bad approach

        var result = bankAccount.Deposit(100);

        Assert.That(result, Is.True);
        Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));
    }

    [Test]
    public void Deposit_ShouldReturnTrue_WhenAdd100()
    {
        var bankAccount = new BankAccount(new Mock<ILogBook>().Object);

        var result = bankAccount.Deposit(100);

        Assert.That(result, Is.True);
        Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));
    }
}
