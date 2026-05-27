using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Contracts.Requests.Customers
{
    public class CreateCustomerRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

       
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [RegularExpression(@"^\+?\d{7,15}$", ErrorMessage = "Phone number must be 7–15 digits and may start with '+'.")]
        public string PhoneNumber { get; set; } = string.Empty;


        [MinLength(1, ErrorMessage = "At least one vehicle is required.")]
        public List<CreateVehicleRequest> Vehicles { get; set; } = [];
    }
}
