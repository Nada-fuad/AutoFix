using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;
namespace AutoFix.Domain.Customers.Vehicles;

public sealed class Vehicle : AuditableEntity
{
    public Guid CustomerId { get; private set; }

    public string Make { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }

    public string LicensePlate { get; private set; }
    public string VehicleInfo => $"{Make} | {Model} | {Year}";

    public Customer? Customer { get; private set; }
    public Vehicle() { }

    public Vehicle(Guid id,  string make, string model, int year, string licensePlate) : base(id) {
        Make = make;
        Model = model;
        Year = year;
        LicensePlate = licensePlate;


    }

    public  static Result<Vehicle> Create(Guid id, string make, string model, int year, string licensePlate)
    {

        if (string.IsNullOrWhiteSpace(make))
        {
            return VehicleErrors.MakeRequired;
        }
        if (string.IsNullOrWhiteSpace(model))
        {
            return VehicleErrors.ModelRequired;
        }
        if (string.IsNullOrWhiteSpace(licensePlate)) {
            return VehicleErrors.LicensePlateRequired;
        }
        if (year < 1886 || year > DateTime.UtcNow.Year)
        {
            return VehicleErrors.YearInvalid;
        }

        return new Vehicle(id, make, model, year, licensePlate);
    }

    public  Result<Updated> Update( string make, string model, int year, string licensePlate)
    {
        if (string.IsNullOrWhiteSpace(make))
        {
            return VehicleErrors.MakeRequired;
        }
        if (string.IsNullOrWhiteSpace(model))
        {
            return VehicleErrors.ModelRequired;
        }
        if (string.IsNullOrWhiteSpace(licensePlate))
        {
            return VehicleErrors.LicensePlateRequired;
        }
        if (year < 1886 || year > DateTime.UtcNow.Year)
        {
            return VehicleErrors.YearInvalid;
        }


        Make = make;

        Model = model;
        Year = year;
        LicensePlate = licensePlate;


        return Result.Updated;

    }

}

