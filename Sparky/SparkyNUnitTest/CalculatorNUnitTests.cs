using NUnit.Framework;

namespace Sparky.NUnitTest;

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

    [Test]
    public void IsOddNumber_OddNumber_ReturnTrue()
    {
        var calculator = new Calculator();

        var result = calculator.IsOddNumber(1);

        Assert.IsTrue(result);
        //Assert.That(result, Is.True);
        //Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    [TestCase(2)]
    [TestCase(4)]
    public void IsOddNumber_EvenNumber_ReturnFalse(int number)
    {
        var calculator = new Calculator();

        var result = calculator.IsOddNumber(number);

        Assert.IsFalse(result);
    }

    [Test]
    [TestCase(10, ExpectedResult = false)]
    [TestCase(3, ExpectedResult = true)]
    public bool IsOddNumber_Number_ReturnTrueIfOdd(int number)
    {
        var calculator = new Calculator();

        return calculator.IsOddNumber(number);
    }

    [Test]
    [TestCase(11.2, 0.8)]
    [TestCase(11.2, 0.2)]
    [TestCase(11.2, 1)]
    public void AddNumbers_TwoDouble_GetCorrectAddition(double a, double b)
    {
        var calculatorMSTests = new Calculator();

        var result = calculatorMSTests.AddNumbers(a, b);

        Assert.AreEqual(12, result, 1); //number between 11-13 will assert
    }

    [Test]
    public void OddRanger_TwoNumbers_ReturnValidOddNumbersRange()
    {
        var calc = new Calculator();
        var expected = new List<int>() { 1, 3, 5, 7, 9 };

        var result = calc.GetOddRange(1, 10);

        Assert.AreEqual(expected, result);
        Assert.That(result, Is.EquivalentTo(expected));
        Assert.Contains(7, result);
        Assert.That(result, Does.Contain(7));
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count, Is.EqualTo(expected.Count));
        Assert.That(result, Is.Ordered);
    }
}
