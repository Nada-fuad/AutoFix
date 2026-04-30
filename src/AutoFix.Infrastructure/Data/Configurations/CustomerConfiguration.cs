using AutoFix.Domain.Customers;
using AutoFix.Domain.Customers.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFix.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

       

        builder.Navigation(c => c.Vehicles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}