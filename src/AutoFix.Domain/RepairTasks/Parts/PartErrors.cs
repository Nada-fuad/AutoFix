using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Domain.RepairTasks.Parts
{
    public static class PartErrors
    {
        public static  readonly Error NameRquired = Error.Validation("Part.Name.Required", "Part name is required.");

        public static readonly Error CostInvalid =
       Error.Validation("Part.Cost.Invalid", "Part cost must be between 1 and 10,000.");

        public static readonly Error QuantityInvalid =
            Error.Validation("Part.Quantity.Invalid", "Quantity must be between 1 and 10.");
    }
    }
