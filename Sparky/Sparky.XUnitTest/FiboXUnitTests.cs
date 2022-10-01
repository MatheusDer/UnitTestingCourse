using Xunit;

namespace Sparky.NUnitTest;

public class FiboXUnitTests
{
    private readonly Fibo fibo;

    public FiboXUnitTests()
    {
        fibo = new Fibo();
    }

    [Fact]
    public void GetFiboSeries_RangeOne_ShouldReturnFiboSeries()
    {
        fibo.Range = 1;
        var expected = new List<int>() { 0 };

        var result = fibo.GetFiboSeries();

        Assert.Multiple(() =>
        {
            Assert.Equal(expected, result);
            Assert.NotEmpty(result);
            Assert.Equal(result.OrderBy(i => i), result);
        });
    }

    [Fact]
    public void GetFiboSeries_RangeSix_ShouldReturnFiboSeries()
    {
        fibo.Range = 6;
        var expected = new List<int>() { 0, 1, 1, 2, 3, 5 };

        var result = fibo.GetFiboSeries();

        Assert.Multiple(() =>
        {
            Assert.Equal(expected, result);
            Assert.Equal(fibo.Range, result.Count);
            Assert.Contains(3, result);
            Assert.DoesNotContain(6, result);
        });
    }

    [Fact]
    public void GetFiboSeries_RangeZero_ShouldReturnEmptyList()
    {
        fibo.Range = 0;

        var result = fibo.GetFiboSeries();

        Assert.Empty(result);
    }
}
