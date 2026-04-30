using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Customers;
using AutoFix.Domain.Customers.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Common.Interfaces
{
    public interface IAppDbContext
    {

        public DbSet<Customer> Customers { get; }

        public DbSet<Vehicle> Vehicles { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
