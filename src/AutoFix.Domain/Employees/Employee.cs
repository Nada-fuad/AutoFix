using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Common;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Identity;

namespace AutoFix.Domain.Employees
{
   public class Employee:AuditableEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }
        public string FullName { get; set; }


        public Employee() { }
        public Employee(Guid id,string firstName, string lastName, Role role):base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            FullName = firstName + " " + lastName;

        }

        public static Result<Employee> Create(Guid id,string firstName,string lastName, Role role)
        {

            if (id == Guid.Empty)
            {
                return EmployeeErrors.IdRequired;
            }

            if (string.IsNullOrWhiteSpace(firstName))
            {
                return EmployeeErrors.FirstNameRequired;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                return EmployeeErrors.LastNameRequired;
            }

            if (!Enum.IsDefined(role))
            {
                return EmployeeErrors.RoleInvalid;
            }

            return new Employee(id, firstName.Trim(), lastName.Trim(), role);
        }
    }
}
