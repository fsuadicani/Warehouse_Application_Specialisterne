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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.City)
                .HasConversion(
                    v => v.value,
                    v => new City(v));

            builder.Property(x => x.Street)
                .HasConversion(
                    v => v.value,
                    v => new StreetName(v));

            builder.Property(x => x.StreetNumber)
                .HasConversion(
                    v => v.value,
                    v => new StreetNumber(v));

            builder.Property(x => x.ZipCode)
                .HasConversion(
                    v => v.value,
                    v => new ZipCode(v));
        }
    }
}