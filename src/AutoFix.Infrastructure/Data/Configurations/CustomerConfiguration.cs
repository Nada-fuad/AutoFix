using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace AutoFix.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.Property(x=>x.Email).IsRequired().HasMaxLength(100);

            builder.Property(x=>x.PhoneNumber).IsRequired().HasMaxLength(100);


            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
