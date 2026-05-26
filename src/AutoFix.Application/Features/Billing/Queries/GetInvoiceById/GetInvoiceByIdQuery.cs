using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.Billing.Dtos;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Application.Features.Billing.Queries.GetInvoiceById
{
   public sealed record GetInvoiceByIdQuery(Guid InvoiceId):ICachedQuery<Result<InvoiceDto>>
    {

        public string CacheKey => $"invoice_{InvoiceId}";

        public TimeSpan Expiration => TimeSpan.FromMinutes(10);

        public string[] Tags => ["invoice"];
    }
}
