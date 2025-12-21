using AutoFix.Domain.Workorders.Billing;

namespace AutoFix.Application.Common.Interfaces;

public interface IInvoicePdfGenerator
{
    byte[] Generate(Invoice invoice);
}