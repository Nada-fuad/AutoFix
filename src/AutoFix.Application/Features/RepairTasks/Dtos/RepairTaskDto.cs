using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.RepairTasks.Enums;
using AutoFix.Domain.RepairTasks.Parts;

namespace AutoFix.Application.Features.RepairTasks.Dtos
{
    public class RepairTaskDto
    {
        public Guid RepairTaskId { get; set; }
        public string Name { get; set; } = string.Empty;
        public RepairDurationInMinutes EstimatedDurationInMins { get; set; }
        public decimal LaborCost { get; set; }
        public decimal TotalCost { get; set; }

        public List<PartDto> Parts { get; set; }

    }
}
