using NUnit.Framework;

namespace Sparky.NUnitTests;

[TestFixture]
public class CalculatorNUnitTests
{
    [Test]
    public void AddNumbers_TwoInt_GetCorrectAddition()
    {
        //Arange
        var calculatorMSTests = new Calculator();

        //Act
        var result = calculatorMSTests.AddNumbers(10, 20);

        //Assert
        Assert.AreEqual(30, result);
    }
}
