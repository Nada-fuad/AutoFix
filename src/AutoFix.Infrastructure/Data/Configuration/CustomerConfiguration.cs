using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFix.Infrastructure.Data.Configuration
{
    public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasMaxLength(255);

            builder.Property(x=> x.Email).HasMaxLength(255);
            builder.Property(x=>x.PhoneNumber).HasMaxLength(255);
        }
    }
}
