using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers.Vehicles;

namespace AutoFix.Domain.Customers
{
    public sealed class Customer : AuditableEntity
    {

        public string? Name { get; private set; }
        public string? PhoneNumber { get; private set; }

        public string? Email { get; private set; }

        private readonly List<Vehicle> _vehicles = [];
        public IReadOnlyCollection<Vehicle> Vehicles => _vehicles.AsReadOnly();
        private Customer() { }
        private Customer(Guid id, string name, string email, string phoneNumber, List<Vehicle> vehicles) : base(id)
        {

            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            _vehicles = vehicles;


        }

        public static Result<Customer> Create(Guid id, string name, string email, string phoneNumber, List<Vehicle> vehicles)
        {
            if (string.IsNullOrWhiteSpace(name)) { return CustomerErrors.NameRequired; }
            if (string.IsNullOrWhiteSpace(email)) { return CustomerErrors.EmailRequired; }

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {

                return CustomerErrors.EmailInvalid;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$")) { return CustomerErrors.PhoneNumberRequired; }



            return new Customer(id, name, email, phoneNumber, vehicles);
        }


        public Result<Updated> Update(string name, string email, string phoneNumber)
        {

            if (string.IsNullOrWhiteSpace(name)) { return CustomerErrors.NameRequired; }
            if (string.IsNullOrWhiteSpace(email)) { return CustomerErrors.EmailRequired; }
            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$")) { return CustomerErrors.PhoneNumberRequired; }


            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;


            return Result.Updated;
        }



        public Result<Updated> UpserParts(List<Vehicle> incomingVehicle)
        {
            _vehicles.RemoveAll(existing => incomingVehicle.All(v => v.Id != existing.Id));

            foreach (var incoming in incomingVehicle)
            {
                var existing = _vehicles.FirstOrDefault(v => v.Id == incoming.Id);
                if (existing is null)
                {
                    _vehicles.Add(incoming);

                }
                else
                {

                    var updatedVehicleResult = existing.Update(incoming.Make, incoming.Model, incoming.Year, incoming.LicensePlate);

                    if (updatedVehicleResult.IsError)
                    {
                        return updatedVehicleResult.Errors;
                    }
                }


            }
            return Result.Updated;

        }
    }
}
