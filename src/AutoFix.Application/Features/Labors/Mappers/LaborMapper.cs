using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Labors.Dtos;
using AutoFix.Domain.Employees;

namespace AutoFix.Application.Features.Labors.Mappers
{
    public static class LaborMapper
    {
         public static LaborDto ToDto(this Employee employee)
        {
            return new LaborDto { LaborId = employee.Id, Name = employee.FullName };
        }

        public static List<LaborDto> ToDtos(this IEnumerable<Employee> entities)
        {
            return [.. entities.Select(l => l.ToDto())];
        }
    }
}
