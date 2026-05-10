using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Contracts.Requests.Customers
{
   public class CreateVehicleRequest
    {
        [Required(ErrorMessage = "Make is required.")]
        public string Make { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Spot is required.")]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
