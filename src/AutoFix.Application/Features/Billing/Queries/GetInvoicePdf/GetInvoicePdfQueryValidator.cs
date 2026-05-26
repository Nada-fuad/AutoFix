using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.Billing.Queries.GetInvoicePdf
{
  public class GetInvoicePdfQueryValidator:AbstractValidator<GetInvoicePdfQuery>
    {

        public GetInvoicePdfQueryValidator()
        {
            RuleFor(request => request.InvoiceId)
                .NotEmpty()
                .WithErrorCode("InvoiceId_Is_Required")
                .WithMessage("InvoiceId is required.");
        }
    }
}
