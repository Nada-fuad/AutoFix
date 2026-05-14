using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.RepairTasks.Enums;
using AutoFix.Domain.RepairTasks.Parts;

namespace AutoFix.Domain.RepairTasks
{
    public sealed class RepairTask:AuditableEntity
    {

        public string Name { get; private set; }
        public decimal LaborCost { get; private set; }
        public RepairDurationInMinutes EstimatedDurationInMins { get; private set; }

        

        private readonly List<Part> _parts = [];
        public IEnumerable<Part> Parts=> _parts.AsReadOnly();
        
        public decimal TotalCost=>LaborCost+Parts.Sum(p => p.Cost*p.Quantity);

        public RepairTask() { }
        public RepairTask(Guid id,string name , decimal laborCost, RepairDurationInMinutes estimatedDurationInMins, List<Part> parts) :base(id) {
        
            Name = name;
            LaborCost = laborCost;
            EstimatedDurationInMins = estimatedDurationInMins;
            _parts = parts;

        }


        public static Result<RepairTask> Create(Guid id, string name,decimal laborCost,RepairDurationInMinutes estimatedDurationInMins, List<Part> parts)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return RepairTaskErrors.NameRequired;
            }
            if (laborCost < 0)
            {
                return RepairTaskErrors.LaborCostInvalid;
            }
            if (!Enum.IsDefined(typeof(RepairDurationInMinutes), estimatedDurationInMins))
            {
                return RepairTaskErrors.DurationInvalid;
            }

            return new RepairTask( id, name.Trim(), laborCost, estimatedDurationInMins, parts);

        }


        public Result<Updated> Update( string name, decimal laborCost, RepairDurationInMinutes estimatedDurationInMins)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return RepairTaskErrors.NameRequired;
            }
            if (laborCost <= 0 || laborCost > 10000)
            {
                return RepairTaskErrors.LaborCostInvalid;
            }
            if (!Enum.IsDefined(typeof(RepairDurationInMinutes), estimatedDurationInMins))
            {
                return RepairTaskErrors.DurationInvalid;
            }

            Name = name.Trim();
            LaborCost = laborCost;
            EstimatedDurationInMins = estimatedDurationInMins;

            return Result.Updated;
        }


        public Result<Updated> UpserParts(List<Part> incomigParts)
        {
            _parts.RemoveAll(existing => incomigParts.All(p=>p.Id== existing.Id));

            foreach(var incoming in incomigParts)
            {
                var existing=_parts.FirstOrDefault(p=>p.Id == incoming.Id);
                if(existing == null)
                {
                    _parts.Add(incoming);
                }
                var updatePartResult = incoming.Update(incoming.Name, incoming.Cost, incoming.Quantity);
                if (updatePartResult.IsError)
                {
                    return updatePartResult.Errors;
                }
            }


            return Result.Updated;
        }
    }
}
