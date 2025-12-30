using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Domain.Customers
{
    public sealed class Customer:AuditableEntity


    {

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        private Customer() { }
        private Customer(Guid id , string name, string phoneNumber,string email):base(id) {
        Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
       
        }

        public static Result<Customer> Create(Guid id, string name, string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return CustomerErrors.NameRequired;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber)) { return CustomerErrors.PhoneNumberRequired; }  
            if (string.IsNullOrWhiteSpace(email)) { return CustomerErrors.EmailRequired; }

            return new Customer(id, name, phoneNumber, email);
        }


        public  Result<Updated> Update(string name, string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return CustomerErrors.NameRequired;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber)) { return CustomerErrors.PhoneNumberRequired; }
            if (string.IsNullOrWhiteSpace(email)) { return CustomerErrors.EmailRequired; }

            Name = name;
            PhoneNumber=phoneNumber;
            Email = email;
                return Result.Updated;

        }


    }
}
