using Xunit;

namespace Sparky.NUnitTest;

public class GrandingCalculatorXUnitTests
{
    private readonly GrandingCalculator _grandingCalculator;

    public GrandingCalculatorXUnitTests()
    {
        _grandingCalculator = new GrandingCalculator();
    }

    [Fact]
    public void GetGrade_ShouldReturnA_WhenInRange()
    {
        _grandingCalculator.Score = 95;
        _grandingCalculator.AttendancePercentage = 90;

        var result = _grandingCalculator.GetGrade();

        Assert.Equal("A", result);
    }

    [Fact]
    public void GetGrade_ShouldReturnB_WhenInRange()
    {
        _grandingCalculator.Score = 81;
        _grandingCalculator.AttendancePercentage = 65;

        var result = _grandingCalculator.GetGrade();

        Assert.Equal("B", result);
    }

    [Fact]
    public void GetGrade_ShouldReturnC_WhenInRange()
    {
        _grandingCalculator.Score = 61;
        _grandingCalculator.AttendancePercentage = 65;

        var result = _grandingCalculator.GetGrade();

        Assert.Equal("C", result);
    }

    [Theory]
    [InlineData(95, 55)]
    [InlineData(55, 95)]
    [InlineData(55, 55)]
    public void GetGrade_ShouldReturnD_WhenInRange(int score, int attendancePercentage)
    {
        _grandingCalculator.Score = score;
        _grandingCalculator.AttendancePercentage = attendancePercentage;

        var result = _grandingCalculator.GetGrade();

        Assert.Equal("D", result);
    }

    [Theory]
    [InlineData(95, 90, "A")]
    [InlineData(81, 65, "B")]
    [InlineData(61, 65, "C")]
    [InlineData(95, 55, "D")]
    [InlineData(55, 95, "D")]
    [InlineData(55, 55, "D")]
    public void Get_Grade_ShouldReturnCorrectGrade_WhenInRange(int score, int attendancePercentage, string expectedResult) //All tests in one
    {
        _grandingCalculator.Score = score;
        _grandingCalculator.AttendancePercentage = attendancePercentage;

        Assert.Equal(expectedResult, _grandingCalculator.GetGrade());
    }
}
