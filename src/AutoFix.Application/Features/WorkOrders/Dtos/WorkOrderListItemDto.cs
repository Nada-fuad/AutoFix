using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.WorkOrders.Enums;

namespace AutoFix.Application.Features.WorkOrders.Dtos
{
    public class WorkOrderListItemDto
    {
        public Guid WorkOrderId { get; set; }
        public Guid? InvoiceId { get; set; }
        public VehicleDto Vehicle { get; set; } = default!;
        public string? Customer { get; set; }
        public string? Labor { get; set; }
        public WorkOrderState State { get; set; }
        public Spot Spot { get; set; }
        public DateTimeOffset StartAtUtc { get; set; }
        public DateTimeOffset EndAtUtc { get; set; }
        public List<string> RepairTasks { get; set; } = [];
    }
}
