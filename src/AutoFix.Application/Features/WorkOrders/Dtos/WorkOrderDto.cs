using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.WorkOrders.Enums;

namespace AutoFix.Application.Features.WorkOrders.Dtos
{
    public sealed record WorkOrderDto
    {
        public Guid WorkOrderId { get; set; }
      
        public VehicleDto? Vehicle { get; set; }

        public string? CustomerName { get; set; }
        public DateTimeOffset StartAtUtc { get; set; }
        public DateTimeOffset EndAtUtc { get; set; }
        public List<RepairTaskDto> RepairTasks { get; set; } = [];
        public WorkOrderState State { get; set; }
      

    }
}
