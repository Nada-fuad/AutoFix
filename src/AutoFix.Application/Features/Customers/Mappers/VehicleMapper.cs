using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Features.Customers.Dtos;
using AutoFix.Domain.Customers.Vehicles;

namespace AutoFix.Application.Features.Customers.Mappers
{
    public static class VehicleMapper
    {
        public static VehicleDto ToDto(this Vehicle vehicle)
        {

            return new VehicleDto(
               vehicle.Id,
                vehicle.Make,
                 vehicle.Model,

                vehicle.Year,
                vehicle.LicensePlate);
        }
    }
}
