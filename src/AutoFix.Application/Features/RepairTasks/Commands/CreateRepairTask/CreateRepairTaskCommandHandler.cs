using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks;
using AutoFix.Domain.RepairTasks.Parts;
using MediatR;

namespace AutoFix.Application.Features.RepairTasks.Commands.CreateRepairTask
{
    public class CreateRepairTaskCommandHandler(IAppDbContext context) : IRequestHandler<CreateRepairTaskCommand, Result<RepairTaskDto>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<RepairTaskDto>> Handle(CreateRepairTaskCommand command, CancellationToken cancellationToken)
        {



           

            List<Part> parts = [];

            foreach (var partCommand in command.Parts
                )
            {
                var partResult = Part.Create(Guid.NewGuid(),partCommand.Name,partCommand.Cost,partCommand.Quantity);

                if (partResult.IsError)
                {

                }

                parts.Add(partResult.Value);
            }
            var repairTasResult = RepairTask.Create(Guid.NewGuid(), command.Name, command.LaborCost, command.EstimatedDurationInMins,parts);
            
            if (repairTasResult.IsError)
            {
            }
            var repairTask = repairTasResult.Value;
            await _context.RepairTasks.AddAsync(repairTask,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = new RepairTaskDto
            {
                RepairTaskId = repairTask.Id,


                Name = command.Name,
                LaborCost = command.LaborCost,
                EstimatedDurationInMins = command.EstimatedDurationInMins,

                Parts = repairTask.Parts.Select(p => new PartDto
                {
                    Name = p.Name,
                    Cost = p.Cost,
                    Quantity = p.Quantity,
                }).ToList(),
            };
            return dto;
            

        }
    }
}
