using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.RepairTasks;
using AutoFix.Domain.RepairTasks.Enums;

namespace AutoFix.Application.Features.RepairTasks.Mappers
{
    public static class RepairTaskMapper
    {

        public static RepairTaskDto ToDto(this RepairTask task)
        {
          return  new RepairTaskDto
            {
               RepairTaskId=task.Id,
               Name=task.Name,
              EstimatedDurationInMins = task.EstimatedDurationInMins,
              LaborCost=task.LaborCost,
              Parts=task.Parts.Select(p=>p.ToDto()).ToList(),

          };


            
    }
    }
}
