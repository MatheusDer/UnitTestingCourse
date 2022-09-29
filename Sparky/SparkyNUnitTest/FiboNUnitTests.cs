using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class FiboNUnitTests
{
    private Fibo fibo;

    [SetUp]
    protected void Setup()
    {
        fibo = new Fibo();
    }

    [Test]
    public void GetFiboSeries_RangeOne_ShouldReturnFiboSeries()
    {
        fibo.Range = 1;
        var expected = new List<int>() { 0 };

        var result = fibo.GetFiboSeries();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Ordered);
        });
    }

    [Test]
    public void GetFiboSeries_RangeSix_ShouldReturnFiboSeries()
    {
        fibo.Range = 6;
        var expected = new List<int>() { 0, 1, 1, 2, 3, 5 };

        var result = fibo.GetFiboSeries();

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result.Count, Is.EqualTo(fibo.Range));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Not.Contain(6));
        });
    }

    [Test]
    public void GetFiboSeries_RangeZero_ShouldReturnEmptyList()
    {
        fibo.Range = 0;

        var result = fibo.GetFiboSeries();

        Assert.That(result, Is.Empty);
    }
}
