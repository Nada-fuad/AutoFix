using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Billing.Dtos;
using AutoFix.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AutoFix.Application.Features.Billing.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.Billing.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQueryHandler(ILogger<GetInvoiceByIdQueryHandler> logger,
     IAppDbContext context) : IRequestHandler<GetInvoiceByIdQuery, Result<InvoiceDto>>
    {
        private readonly ILogger<GetInvoiceByIdQueryHandler> _logger = logger;
        private readonly IAppDbContext _context = context;

        public async Task<Result<InvoiceDto>> Handle(GetInvoiceByIdQuery query, CancellationToken ct)
        {
            var invoice = await _context.Invoices.AsNoTracking()
             .Include(i => i.LineItems)
             .Include(i => i.WorkOrder!)
                 .ThenInclude(w => w.Vehicle!)
                     .ThenInclude(v => v.Customer)
             .FirstOrDefaultAsync(i => i.Id == query.InvoiceId, ct);

            if (invoice is null)
            {
                _logger.LogWarning("Invoice not found. InvoiceId: {InvoiceId}", query.InvoiceId);
                return Error.NotFound("Invoice not found.");
            }

            return invoice.ToDto();
        }
    }
}
