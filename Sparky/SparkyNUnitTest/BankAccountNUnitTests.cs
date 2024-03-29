﻿using Moq;
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

    [Test]
    public void LogDummy_ShouldReturnTrue_WhenLockMockString()
    {
        var logMock = new Mock<ILogBook>();
        var desiredOutput = "hello";

        logMock.Setup(l => l.MessageWithReturn(It.IsAny<string>())).Returns((string s) => s.ToLower()); //Ur implementing the behavior of the method

        Assert.That(logMock.Object.MessageWithReturn("HEllo"), Is.EqualTo(desiredOutput));
    }

    [Test]
    public void LogDummy_ShouldReturnTrue_WhenLockMockStringOutput()
    {
        var logMock = new Mock<ILogBook>();
        var input = "A";
        string desiredOutput = "Hello " + input;

        logMock.Setup(l => l.LogWithOutputResult(input, out desiredOutput)).Returns(true); 

        Assert.IsTrue(logMock.Object.LogWithOutputResult(input, out string result));
        Assert.That(result, Is.EqualTo(desiredOutput));
    }

    [Test]
    public void LogDummy_ShouldReturnTrue_WhenLockMockRef()
    {
        var logMock = new Mock<ILogBook>();
        var usedCustomer = new Customer();
        var customer = new Customer();

        logMock.Setup(l => l.LogWithRefObj(ref usedCustomer)).Returns(true);

        Assert.IsTrue(logMock.Object.LogWithRefObj(ref usedCustomer));
        Assert.IsFalse(logMock.Object.LogWithRefObj(ref customer));
    }

    [Test]
    public void LogDummy_ShouldMockTest_WhenSetAndGetLogTypeAndSeverityMock()
    {
        var logMock = new Mock<ILogBook>();

        logMock.Setup(l => l.LogSeverity).Returns(10);
        logMock.Setup(l => l.LogType).Returns("warning");

        Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
        Assert.That(logMock.Object.LogType, Is.EqualTo("warning"));

        logMock.SetupAllProperties(); //needs this to acctualy set the property values
        logMock.Object.LogSeverity = 100;

        Assert.That(logMock.Object.LogSeverity, Is.EqualTo(100));

        //Callbacks
        var logTemp = "Hello, ";
        logMock.Setup(l => l.LogToDb(It.IsAny<string>()))
            .Returns(true)
            .Callback((string str) => logTemp += str);

        logMock.Object.LogToDb("A");
        Assert.That(logTemp, Is.EqualTo("Hello, A"));

        var counter = 5;
        logMock.Setup(l => l.LogToDb(It.IsAny<string>()))
            .Returns(true)
            .Callback(() => counter++);

        logMock.Object.LogToDb("A");
        logMock.Object.LogToDb("A");
        logMock.Object.LogToDb("A");
        logMock.Object.LogToDb("A");

        Assert.That(counter, Is.EqualTo(9));
    }

    [Test]
    public void BankLogDummy_VerifyExample()
    {
        var logMock = new Mock<ILogBook>();
        var account = new BankAccount(logMock.Object);

        account.Deposit(100);
        Assert.That(account.GetBalance, Is.EqualTo(100));

        //verification
        logMock.Verify(l => l.Message(It.IsAny<string>()), Times.Exactly(2));
        logMock.VerifySet(l => l.LogSeverity = 101, Times.Once);
    }
}
