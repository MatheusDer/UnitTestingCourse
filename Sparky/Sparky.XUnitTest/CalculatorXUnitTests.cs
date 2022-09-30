using Xunit;

namespace Sparky.NUnitTest;

public class CalculatorXUnitTests
{
    [Fact]
    public void AddNumbers_TwoInt_GetCorrectAddition()
    {
        //Arange
        var calculatorMSTests = new Calculator();

        //Act
        var result = calculatorMSTests.AddNumbers(10, 20);

        //Assert
        Assert.Equal(30, result);
    }

    [Fact]
    public void IsOddNumber_OddNumber_ReturnTrue()
    {
        var calculator = new Calculator();

        var result = calculator.IsOddNumber(1);

        Assert.True(result);
        //Assert.That(result, Is.True);
        //Assert.That(result, Is.EqualTo(true));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(4)]
    public void IsOddNumber_EvenNumber_ReturnFalse(int number)
    {
        var calculator = new Calculator();

        var result = calculator.IsOddNumber(number);

        Assert.False(result);
    }

    [Theory]
    [InlineData(10, false)]
    [InlineData(3, true)]
    public void IsOddNumber_Number_ReturnTrueIfOdd(int number, bool expectedResult)
    {
        var calculator = new Calculator();

        var result =  calculator.IsOddNumber(number);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(11.2, 0.8)]
    public void AddNumbers_TwoDouble_GetCorrectAddition(double a, double b)
    {
        var calculatorMSTests = new Calculator();

        var result = calculatorMSTests.AddNumbers(a, b);

        Assert.Equal(12, result, 1); //number between 11-13 will assert
    }

    [Fact]
    public void OddRanger_TwoNumbers_ReturnValidOddNumbersRange()
    {
        var calc = new Calculator();
        var expected = new List<int>() { 1, 3, 5, 7, 9 };

        var result = calc.GetOddRange(1, 10);

        Assert.Equal(expected, result);
        Assert.Contains(7, result);
        Assert.DoesNotContain(10, result);
        Assert.NotEmpty(result);
        Assert.Equal(expected.Count, result.Count);
        Assert.Equal(result.OrderBy(x => x), result);
    }
}
