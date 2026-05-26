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
        public static Error CustomerNotFound=> Error.NotFound(
           "ApplicationErrors.Customer.NotFound",
           "Customer does not exist.");
        public static Error WorkOrderNotFound=> Error.NotFound(
           "ApplicationErrors.WorkOrder.NotFound",
           "WorkOrder does not exist.");

        public static Error AtLeastOneRepairTaskIsRequired =>
       Error.Validation(
           code: "RepairTask.Required",
          message: "At least one repair task must be specified.");

        public static Error RepairTaskNotFound =>
    Error.NotFound(
            "RepairTask.NotFound",
            "Repair task does not exist.");


        public static Error LaborNotFound =>
       Error.NotFound("Employee.LaborNotFound", "Labor does not exist.");

        public static Error WorkOrderMustBeCompletedForInvoicing =>
   Error.Conflict(
           "WorkOrder.InvoiceIssuance.InvalidState",
           "WorkOrder must be in 'Completed' state to issue an invoice.");



        public static Error InvoiceNotFound => Error.NotFound(
      "ApplicationErrors.Invoice.NotFound",
      "Invoice does not exist.");
    }



 
    }
