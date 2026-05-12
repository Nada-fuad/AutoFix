using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Application.Common.Errors
{
    public static class ApplicationErrors
    {
        public static Error WorkOrderNotFound=> Error.NotFound(
           "ApplicationErrors.WorkOrder.NotFound",
           "WorkOrder does not exist.");
    }
}
