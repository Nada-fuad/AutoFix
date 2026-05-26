using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Billing.Commands.SettleInvoice
{
   public sealed record SettleInvoiceCommand(Guid InvoiceId):IRequest<Result<Success>>;
   
}
