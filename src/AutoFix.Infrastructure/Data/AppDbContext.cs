using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common;
using AutoFix.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Infrastructure.Data
{
    public sealed class AppDbContext : DbContext, IAppDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Customer> Customers => Set<Customer>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            builder.Ignore<DomainEvent>();
        }
    }
}
