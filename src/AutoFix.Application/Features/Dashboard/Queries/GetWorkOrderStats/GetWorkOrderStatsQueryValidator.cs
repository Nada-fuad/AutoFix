using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace AutoFix.Application.Features.Dashboard.Queries.GetWorkOrderStats
{
  public class GetWorkOrderStatsQueryValidator : AbstractValidator<GetWorkOrderStatsQuery>
    {
        public GetWorkOrderStatsQueryValidator()
        {
            RuleFor(request => request.Date)
                .NotEmpty()
                .WithErrorCode("Date_Is_Required")
                .WithMessage("Date is required.");
        }
    }
}
