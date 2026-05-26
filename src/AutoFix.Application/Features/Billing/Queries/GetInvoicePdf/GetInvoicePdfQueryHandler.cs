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
using  Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.Billing.Queries.GetInvoicePdf
{
    public class GetInvoicePdfQueryHandler(ILogger<GetInvoicePdfQuery> logger,IAppDbContext context, IInvoicePdfGenerator pdfGenerator) : IRequestHandler<GetInvoicePdfQuery, Result<InvoicePdfDto>>
    {
        private readonly ILogger<GetInvoicePdfQuery> _logger = logger;
        private readonly IAppDbContext _context = context;

        public async Task<Result<InvoicePdfDto>> Handle(GetInvoicePdfQuery query, CancellationToken ct)
        {
            var invoice = await _context.Invoices.AsNoTracking()
                  .Include(i => i.LineItems)
          .FirstOrDefaultAsync(i => i.Id == query.InvoiceId, ct);

            if (invoice is null)
            {
                logger.LogWarning("Invoice not found. InvoiceId: {InvoiceId}", query.InvoiceId);
                return Error.NotFound("Invoice not found.");
            }

            try
            {
                var pdfBytes = pdfGenerator.Generate(invoice);

                var invoicePdf = new InvoicePdfDto
                {
                    Content = pdfBytes,
                    FileName = $"invoice-{invoice.Id}.pdf"
                };

                return invoicePdf;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to generate PDF for InvoiceId: {InvoiceId}", query.InvoiceId);
                return Error.Failure("An error occurred while generating the invoice PDF.");
            }
        }
    }
}
