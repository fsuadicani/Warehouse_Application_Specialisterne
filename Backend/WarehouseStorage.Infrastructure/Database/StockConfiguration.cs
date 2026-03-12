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
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.InHouseLocation)
                .HasConversion(
                    v => v.value,
                    v => new StockLocation(v));

            builder.Property(x => x.Quantity)
                .HasConversion(
                    v => v.value,
                    v => new Quantity(v));

            builder.Property(x => x.LocalPrice)
                .HasConversion(
                    v => v.value,
                    v => new Price(v));

            builder.Property(x => x.LocalCurrency)
                .HasConversion(
                    v => v.value,
                    v => new Currency(v));

                    // Relationship: Stock -> Location
            builder.HasOne(s => s.Location);
        }
    }
}