using AutoFix.Domain.Customers.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFix.Infrastructure.Data.Configurations;

public sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Make)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Model)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.LicensePlate)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Year)
            .IsRequired();
    }
}