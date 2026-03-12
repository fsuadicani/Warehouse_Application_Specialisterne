using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Models;

namespace WarehouseStorage.Infrastructure.Database
{
    public class TransitConfiguration : IEntityTypeConfiguration<Transit>
    {
        public void Configure(EntityTypeBuilder<Transit> builder)
        {
            builder.Property(x => x.TransitNumber)
                .HasConversion(
                    v => v.value,
                    v => new TransitNumber(v));

            builder.Property(x => x.PickUpCode)
                .HasConversion(
                    v => v.value,
                    v => new PickUpCode(v));

            builder.Property(x => x.GpsLocation)
                .HasConversion(
                    v => v.value,
                    v => new Coordinates(v));

            builder.Property(x => x.Distributor)
                .HasConversion(
                    v => v.value,
                    v => new CompanyName(v));

            builder.Property(x => x.DeliveryStatus)
              .HasConversion<string>();

            //RElationships
            builder.HasMany(l => l.StockLocations)
                .WithOne()
                .HasForeignKey("ReferenceId");
        }
    }
}