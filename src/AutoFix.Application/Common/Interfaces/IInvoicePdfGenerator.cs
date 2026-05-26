using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.WorkOrders.Billing;

namespace AutoFix.Application.Common.Interfaces
{
   public interface IInvoicePdfGenerator
    {
        byte[] Generate(Invoice invoice);
    }
}
