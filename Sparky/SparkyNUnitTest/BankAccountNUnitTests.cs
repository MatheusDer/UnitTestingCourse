using Moq;
using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class BankAccountNUnitTests
{
    [Test]
    public void Deposit_ShouldReturnTrue_WhenAdd100()
    {
        var bankAccount = new BankAccount(new Mock<ILogBook>().Object);

        var result = bankAccount.Deposit(100);

        Assert.That(result, Is.True);
        Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));
    }

    [Test]
    public void Withdraw_ShouldReturnTrue_WhenWithdraw100With200Balance()
    {
        var logMock = new Mock<ILogBook>();
        logMock.Setup(l => l.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(l => l.LogBalanceAfterWithdrawl(It.Is<int>(n => n >= 0))).Returns(true);

        var bankAccount = new BankAccount(logMock.Object);
        bankAccount.Deposit(200);

        var result = bankAccount.Withdraw(100);

        Assert.IsTrue(result);
    }

    [Test]
    public void Withdraw_ShouldReturnFalse_WhenWithdraw300With200Balance()
    {
        var logMock = new Mock<ILogBook>();
        logMock.Setup(l => l.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(l => l.LogBalanceAfterWithdrawl(It.Is<int>(n => n <= 0))).Returns(false);

        var bankAccount = new BankAccount(logMock.Object);
        bankAccount.Deposit(200);

        var result = bankAccount.Withdraw(300);

        Assert.IsFalse(result);
    }
}
