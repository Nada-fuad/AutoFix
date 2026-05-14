using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Mappers;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Application.Features.RepairTasks.Mappers;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Domain.Common;
using AutoFix.Domain.Customers.Vehicles;
using AutoFix.Domain.WorkOrders;

namespace AutoFix.Application.Features.WorkOrders.Mappers
{
   public static class WorkOrderMappercs
    {

        public static WorkOrderDto ToDto(this WorkOrder workOrder)
        {
            return new WorkOrderDto
            {
                WorkOrderId = workOrder.Id,
                Vehicle = workOrder.Vehicle is null ? null : workOrder.Vehicle.ToDto(),
                CustomerName = workOrder.Vehicle.Customer.Name,
                StartAtUtc = workOrder.StartAtUtc,
                EndAtUtc = workOrder.EndAtUtc,
                RepairTasks = workOrder.RepairTasks.Select(repairTask => repairTask.ToDto()).ToList(),
                State = workOrder.State,
                LaborId = workOrder.LaborId


            };

        }
       
    }
   
    }
