using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Contracts.Requests.WorkOrders
{
    public class CreateWorkOrderRequest
    {


        [Required(ErrorMessage = "Vehicle is required.")]
        public Guid VehicleId { get; set; }

        [MinLength(1, ErrorMessage = "At least one repair task must be selected.")]
        public List<Guid> RepairTaskIds { get; set; } = [];

        [Required(ErrorMessage = "Labor is required.")]
        public Guid LaborId { get; set; }

        [Required(ErrorMessage = "StartAt is required.")]
        public DateTimeOffset StartAtUtc { get; set; }
    }
}
