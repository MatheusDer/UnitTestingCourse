using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky;

public class GrandingCalculator
{
    public int Score { get; set; }
    public int AttendancePercentage { get; set; }

    public string GetGrade()
    {
        if (Score > 90 && AttendancePercentage > 70)
            return "A";

        if (Score > 80 && AttendancePercentage > 60)
            return "B";

        if (Score > 60 && AttendancePercentage > 60)
            return "C";

        return "D";
    }
}
