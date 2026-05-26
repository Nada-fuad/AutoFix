using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.WorkOrders.Dtos;
using AutoFix.Application.Features.WorkOrders.Mappers;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.WorkOrders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Features.WorkOrders.Commands.CreateWorkOrder
{
    public class CreateWorkOrderCommandHandler(IAppDbContext context) : IRequestHandler<CreateWorkOrderCommand, Result<WorkOrderDto>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<WorkOrderDto>> Handle(CreateWorkOrderCommand command, CancellationToken ct)
        {

            var repairTasks= await _context.RepairTasks.Where(x=>command.RepairTaskIds.Contains(x.Id)).ToListAsync(ct);

            if(repairTasks.Count != command.RepairTaskIds.Count) {
                return WorkOrderErrors.RepairTasksRequired;
            }

            var endAt = command.StartAt.AddMinutes(4);

            var workOrderResult = WorkOrder.Create(Guid.NewGuid(), command.VehicleId, command.StartAt, endAt, command.LaborId,command.Spot, repairTasks);

            if (workOrderResult.IsError)
            {
                return workOrderResult.Errors;
            }

            var workOrder = workOrderResult.Value;
            _context.WorkOrders.Add(workOrder);

            await _context.SaveChangesAsync(ct);

            var createWorkOrder = await _context.WorkOrders.Include(w => w.Vehicle).ThenInclude(v => v.Customer).Include(w => w.RepairTasks).FirstOrDefaultAsync(w => w.Id == workOrder.Id, ct);

            return createWorkOrder.ToDto();

        }
    }
}
