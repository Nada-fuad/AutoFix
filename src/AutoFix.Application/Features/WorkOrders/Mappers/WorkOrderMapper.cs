using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Mappers;
using AutoFix.Application.Features.Labors.Dtos;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Application.Features.RepairTasks.Mappers;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Domain.Common;
using AutoFix.Domain.Customers.Vehicles;
using AutoFix.Domain.WorkOrders;

namespace AutoFix.Application.Features.WorkOrders.Mappers
{
   public static class WorkOrderMapper
    {

        public static WorkOrderDto ToDto(this WorkOrder entity)
        {

            ArgumentNullException.ThrowIfNull(entity);

            return new WorkOrderDto
            {
                WorkOrderId = entity.Id,
                Spot = entity.Spot,
                StartAtUtc = entity.StartAtUtc,
                EndAtUtc = entity.EndAtUtc,
                Labor = entity.Labor is null ? null : new LaborDto
                {
                    LaborId = entity.LaborId.Value,
                    Name = $"{entity.Labor.FirstName} {entity.Labor.LastName}"
                },
                RepairTasks = entity.RepairTasks.ToDtos(),
                Vehicle = entity.Vehicle is null ? null : entity.Vehicle.ToDto(),
                State = entity.State,
                TotalPartCost = entity.RepairTasks.SelectMany(t => t.Parts).Sum(p => p.Cost * p.Quantity),
                TotalLaborCost = entity.RepairTasks.Sum(p => p.LaborCost),
                TotalCost = entity.RepairTasks.Sum(rt => rt.TotalCost),
                TotalDurationInMins = entity.RepairTasks.Sum(rt => (int)rt.EstimatedDurationInMins),
                InvoiceId = entity.Invoice?.Id,
                CreatedAt = entity.CreatedAtUtc
            };
        }
       
    }
   
    }
