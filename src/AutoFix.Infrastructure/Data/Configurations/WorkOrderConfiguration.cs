using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Domain.WorkOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFix.Infrastructure.Data.Configurations
{
   public class WorkOrderConfiguration
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder) { 
        builder.HasKey(w => w.Id).IsClustered(false);

        builder.Property(w => w.LaborId)
               .IsRequired();

        builder.HasOne(w => w.Labor).WithMany().HasForeignKey(w => w.LaborId).IsRequired();

       

        builder.Property(w => w.State).HasConversion<string>().IsRequired();

        builder.Property(w => w.StartAtUtc).IsRequired();

        builder.Property(w => w.EndAtUtc).IsRequired();

        

        builder
            .HasMany(w => w.RepairTasks)
            .WithMany()
            .UsingEntity(j => j.ToTable("WorkOrderRepairTasks"));

        builder.HasOne(w => w.Vehicle)
               .WithMany()
               .HasForeignKey(w => w.VehicleId);

        builder.HasIndex(w => w.LaborId);
        builder.HasIndex(w => w.VehicleId);
        builder.HasIndex(w => w.State);
        builder.HasIndex(a => new { a.StartAtUtc, a.EndAtUtc
    });


        }
    }
}
