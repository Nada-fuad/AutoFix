using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Customers;

namespace AutoFix.Application.Features.Customers.Mappers
{
    public static class CustomerMapper
    {

        public static CustomerDto ToDto(Customer customer)
        {

            return new CustomerDto
            {

                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,

                Vehicles=customer.Vehicles.Select(v=>new VehicleDto(

                      v.Id,
                   v.Make,
                    v.Model,
                  v.Year,
                     v.LicensePlate
                    )

                
                   
                ).ToList()
            };

        }
    }
}
