using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers.Vehicles;

namespace AutoFix.Tests.Common.Customers
{
    public static class VehicleFactory
    {

        public static Result<Vehicle> CreateVehicle(Guid? id = null, string? make = null, string? model = null, int? year = null, string? licensePlate = null)
        {
            return Vehicle.Create(
                id ?? Guid.NewGuid(),
                make ?? "Honda",
                model ?? "Accord",
                year ?? 2024,
                licensePlate ?? "ABC 123");
        }
    }
}
