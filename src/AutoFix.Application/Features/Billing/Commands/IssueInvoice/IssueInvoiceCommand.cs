using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Billing.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;

namespace AutoFix.Application.Features.Billing.Commands.IssueInvoice
{
   public sealed record IssueInvoiceCommand(Guid WorkOrderId):IRequest<Result<InvoiceDto>>
    {
    }
}
