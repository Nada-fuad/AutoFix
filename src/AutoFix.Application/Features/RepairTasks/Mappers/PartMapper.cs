using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.RepairTasks.Dtos;
using AutoFix.Domain.RepairTasks.Parts;

namespace AutoFix.Application.Features.RepairTasks.Mappers
{
   public static class PartMapper
    {
        public static PartDto ToDto(this Part part)
        {

            return new PartDto
            {
                PartId = part.Id,
                Name = part.Name,
                Cost = part.Cost,
                Quantity = part.Quantity

            };
        }
    }
}
