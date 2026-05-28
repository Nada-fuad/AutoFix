using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Contracts.Common;

namespace AutoFix.Contracts.Requests.WorkOrders
{
    public record AssignLaborRequest
    {
        [Required(ErrorMessage = "LaborId is required.")]
        public string LaborId { get; set; } = string.Empty;
    }
}
