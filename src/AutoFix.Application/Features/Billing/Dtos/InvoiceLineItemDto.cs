using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Application.Features.Billing.Dtos
{
    public record InvoiceLineItemDto
    {
        public Guid InvoiceId { get; set; }
        public int LineNumber { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }
}
