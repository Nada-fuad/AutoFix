using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Contracts.Responses
{
    public sealed record OperatingHoursResponse(TimeOnly OpeningTime, TimeOnly ClosingTime);
}
