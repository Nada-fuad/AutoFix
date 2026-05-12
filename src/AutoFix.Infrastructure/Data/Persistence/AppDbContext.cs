using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Customers;
using AutoFix.Domain.Customers.Vehicles;
using AutoFix.Domain.Employees;
using AutoFix.Domain.RepairTasks;
using AutoFix.Domain.RepairTasks.Parts;
using AutoFix.Domain.WorkOrders;
using AutoFix.Infrastructure.Data.Configurations;
using AutoFix.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Infrastructure.Data.Persistence
{
    public class AppDbContext : IdentityDbContext<AppUser>, IAppDbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
        public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
        public DbSet<RepairTask> RepairTasks => Set<RepairTask>();

        public DbSet<Part> Parts => Set<Part>();

        public DbSet<Employee> Employees => Set<Employee>();

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        }
    }
}
