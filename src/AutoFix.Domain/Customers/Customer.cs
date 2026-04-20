using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;

namespace AutoFix.Domain.Customers
{
    public sealed class Customer:AuditableEntity
    {

        public string? Name { get; private set; }
        public string? PhoneNumber { get; private set; }

        public string? Email { get; private set; }


        private Customer() { }
        private Customer(Guid id,string name,string email,string phoneNumber):base(id) {
        
            Name = name;    
            Email = email;
            PhoneNumber = phoneNumber;
        
        
        }

        public static Result<Customer> Create(Guid id,string name, string email, string phoneNumber)
        {
            if(string.IsNullOrWhiteSpace(name)) { return CustomerErrors.NameRequired; }
            if (string.IsNullOrWhiteSpace(email)) {  return CustomerErrors.EmailRequired; }

            try
            {
                _ = new MailAddress(email);
            }
            catch
            {

                return CustomerErrors.EmailInvalid;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$")) {  return CustomerErrors.PhoneNumberRequired; }  
           



            return new Customer(id, name, email, phoneNumber);
        }


        public  Result<Updated> Update(string name, string email, string phoneNumber)
        {

            if (string.IsNullOrWhiteSpace(name)) { return CustomerErrors.NameRequired; }
            if (string.IsNullOrWhiteSpace(email)) { return CustomerErrors.EmailRequired; }
            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$")) { return CustomerErrors.PhoneNumberRequired; }


            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;


            return Result.Updated;
        }


       
    }
}
