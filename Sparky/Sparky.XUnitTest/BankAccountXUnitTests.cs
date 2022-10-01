using Moq;
using Xunit;

namespace Sparky.NUnitTest;

public class BankAccountXUnitTests
{
    [Fact]
    public void Deposit_ShouldReturnTrue_WhenAdd100()
    {
        var bankAccount = new BankAccount(new Mock<ILogBook>().Object);

        var result = bankAccount.Deposit(100);

        Assert.True(result);
        Assert.Equal(100, bankAccount.GetBalance());
    }

    [Fact]
    public void Withdraw_ShouldReturnTrue_WhenWithdraw100With200Balance()
    {
        var logMock = new Mock<ILogBook>();
        logMock.Setup(l => l.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(l => l.LogBalanceAfterWithdrawl(It.Is<int>(n => n >= 0))).Returns(true);

        var bankAccount = new BankAccount(logMock.Object);
        bankAccount.Deposit(200);

        var result = bankAccount.Withdraw(100);

        Assert.True(result);
    }

    [Fact]
    public void Withdraw_ShouldReturnFalse_WhenWithdraw300With200Balance()
    {
        var logMock = new Mock<ILogBook>();
        logMock.Setup(l => l.LogToDb(It.IsAny<string>())).Returns(true);
        logMock.Setup(l => l.LogBalanceAfterWithdrawl(It.Is<int>(n => n <= 0))).Returns(false);

        var bankAccount = new BankAccount(logMock.Object);
        bankAccount.Deposit(200);

        var result = bankAccount.Withdraw(300);

        Assert.False(result);
    }

    [Fact]
    public void LogDummy_ShouldReturnTrue_WhenLockMockString()
    {
        var logMock = new Mock<ILogBook>();
        var desiredOutput = "hello";

        logMock.Setup(l => l.MessageWithReturn(It.IsAny<string>())).Returns((string s) => s.ToLower()); //Ur implementing the behavior of the method

        Assert.Equal(desiredOutput, logMock.Object.MessageWithReturn("HEllo"));
    }

    [Fact]
    public void LogDummy_ShouldReturnTrue_WhenLockMockStringOutput()
    {
        var logMock = new Mock<ILogBook>();
        var input = "A";
        string desiredOutput = "Hello " + input;

        logMock.Setup(l => l.LogWithOutputResult(input, out desiredOutput)).Returns(true);

        Assert.True(logMock.Object.LogWithOutputResult(input, out string result));
        Assert.Equal(desiredOutput, result);
    }

    [Fact]
    public void LogDummy_ShouldReturnTrue_WhenLockMockRef()
    {
        var logMock = new Mock<ILogBook>();
        var usedCustomer = new Customer();
        var customer = new Customer();

        logMock.Setup(l => l.LogWithRefObj(ref usedCustomer)).Returns(true);

        Assert.True(logMock.Object.LogWithRefObj(ref usedCustomer));
        Assert.False(logMock.Object.LogWithRefObj(ref customer));
    }

    [Fact]
    public void LogDummy_ShouldMockTest_WhenSetAndGetLogTypeAndSeverityMock()
    {
        var logMock = new Mock<ILogBook>();

        logMock.Setup(l => l.LogSeverity).Returns(10);
        logMock.Setup(l => l.LogType).Returns("warning");

        Assert.Equal(10, logMock.Object.LogSeverity);
        Assert.Equal("warning", logMock.Object.LogType);

        logMock.SetupAllProperties(); //needs this to acctualy set the property values
        logMock.Object.LogSeverity = 100;

        Assert.Equal(100, logMock.Object.LogSeverity);

        //Callbacks
        var logTemp = "Hello, ";
        logMock.Setup(l => l.LogToDb(It.IsAny<string>()))
            .Returns(true)
            .Callback((string str) => logTemp += str);

        logMock.Object.LogToDb("A");
        Assert.Equal("Hello, A", logTemp);

        var counter = 5;
        logMock.Setup(l => l.LogToDb(It.IsAny<string>()))
            .Returns(true)
            .Callback(() => counter++);

        logMock.Object.LogToDb("A");
        logMock.Object.LogToDb("A");
        logMock.Object.LogToDb("A");
        logMock.Object.LogToDb("A");

        Assert.Equal(9, counter);
    }

    [Fact]
    public void BankLogDummy_VerifyExample()
    {
        var logMock = new Mock<ILogBook>();
        var account = new BankAccount(logMock.Object);

        account.Deposit(100);
        Assert.Equal(100, account.GetBalance());

        //verification
        logMock.Verify(l => l.Message(It.IsAny<string>()), Times.Exactly(2));
        logMock.VerifySet(l => l.LogSeverity = 101, Times.Once);
    }
}
