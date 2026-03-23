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
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasOne(l => l.Location)
                .WithMany();
        }
    }
}