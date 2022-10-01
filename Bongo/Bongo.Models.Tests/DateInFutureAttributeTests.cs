using Bongo.Models.ModelValidations;
using Xunit;

namespace Bongo.Models;

public class DateInFutureAttributeTests
{
    [Fact]
    public void DateValidator_ReturnsValid_WhenDateIsValid()
    {
        var dateInFutureAttribute = new DateInFutureAttribute();
    }
}
