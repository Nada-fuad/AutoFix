using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Customers;
using AutoFix.Domain.Customers.Vehicles;
using AutoFix.Domain.Employees;
using AutoFix.Domain.Identity;
using AutoFix.Domain.RepairTasks;
using AutoFix.Domain.RepairTasks.Parts;
using AutoFix.Domain.WorkOrders;
using AutoFix.Domain.WorkOrders.Billing;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Application.Common.Interfaces
{
    public interface IAppDbContext
    {

        public DbSet<Customer> Customers { get; }

        public DbSet<Vehicle> Vehicles { get; }

        public DbSet<WorkOrder> WorkOrders { get; }

        public DbSet<RepairTask> RepairTasks { get; }

        public DbSet<Part> Parts { get; }

        public DbSet<Employee> Employees { get; }
        public DbSet<Invoice> Invoices { get; }
        public DbSet<RefreshToken> RefreshTokens { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
