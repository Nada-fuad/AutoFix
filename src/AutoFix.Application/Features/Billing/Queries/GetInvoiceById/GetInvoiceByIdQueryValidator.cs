using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.Billing.Queries.GetInvoiceById
{
    public sealed class GetInvoiceByIdQueryValidator:AbstractValidator<GetInvoiceByIdQuery>
    {

        public GetInvoiceByIdQueryValidator()
        {
            RuleFor(request => request.InvoiceId)
                .NotEmpty()
                .WithErrorCode("InvoiceId_Is_Required")
                .WithMessage("InvoiceId is required.");
        }
    }
}
