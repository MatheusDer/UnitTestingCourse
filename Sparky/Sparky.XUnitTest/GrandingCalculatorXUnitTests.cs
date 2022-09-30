//using NUnit.Framework;

//namespace Sparky.NUnitTest;

//[TestFixture]
//public class GrandingCalculatorXUnitTests
//{
//    private GrandingCalculator grandingCalculator;

//    [SetUp]
//    public void Setup()
//    {
//        grandingCalculator = new GrandingCalculator();
//    }

//    [Test]
//    public void GetGrade_ShouldReturnA_WhenInRange()
//    {
//        grandingCalculator.Score = 95;
//        grandingCalculator.AttendancePercentage = 90;

//        var result = grandingCalculator.GetGrade();

//        Assert.AreEqual("A", result);
//    }

//    [Test]
//    public void GetGrade_ShouldReturnB_WhenInRange()
//    {
//        grandingCalculator.Score = 81;
//        grandingCalculator.AttendancePercentage = 65;

//        var result = grandingCalculator.GetGrade();

//        Assert.AreEqual("B", result);
//    }

//    [Test]
//    public void GetGrade_ShouldReturnC_WhenInRange()
//    {
//        grandingCalculator.Score = 61;
//        grandingCalculator.AttendancePercentage = 65;

//        var result = grandingCalculator.GetGrade();

//        Assert.AreEqual("C", result);
//    }

//    [Test]
//    [TestCase(95, 55)]
//    [TestCase(55, 95)]
//    [TestCase(55, 55)]
//    public void GetGrade_ShouldReturnD_WhenInRange(int score, int attendancePercentage)
//    {
//        grandingCalculator.Score = score;
//        grandingCalculator.AttendancePercentage = attendancePercentage;

//        var result = grandingCalculator.GetGrade();

//        Assert.AreEqual("D", result);
//    }

//    [Test]
//    [TestCase(95, 90, ExpectedResult = "A")]
//    [TestCase(81, 65, ExpectedResult = "B")]
//    [TestCase(61, 65, ExpectedResult = "C")]
//    [TestCase(95, 55, ExpectedResult = "D")]
//    [TestCase(55, 95, ExpectedResult = "D")]
//    [TestCase(55, 55, ExpectedResult = "D")]
//    public string Get_Grade_ShouldReturnCorrectGrade_WhenInRange(int score, int attendancePercentage) //All tests in one
//    {
//        grandingCalculator.Score = score;
//        grandingCalculator.AttendancePercentage = attendancePercentage;

//        return grandingCalculator.GetGrade();
//    }
//}
