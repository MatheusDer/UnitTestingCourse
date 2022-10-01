using Bongo.Models.ModelValidations;
using NUnit.Framework;

namespace Bongo.Models.Test;

[TestFixture]
public class DateInFutureAttributeTests
{
    [Test]
    [TestCase(1, ExpectedResult = true)]
    [TestCase(-1, ExpectedResult = false)]
    [TestCase(0, ExpectedResult = false)]
    public bool DateValidator_ReturnsValidity_WhenExpectedDate(int seconds)
    {
        var atrribute = new DateInFutureAttribute();

        return atrribute.IsValid(DateTime.Now.AddSeconds(seconds));
    }

    [Test]
    public void DateValidator_ReturnsErrorMessage()
    {
        var atrribute = new DateInFutureAttribute();

        Assert.That(atrribute.ErrorMessage, Is.EqualTo("Date must be in the future"));
    }
}
