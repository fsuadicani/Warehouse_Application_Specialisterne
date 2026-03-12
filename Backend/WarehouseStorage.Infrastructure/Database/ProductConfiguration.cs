using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Models;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasConversion(
                v => v.value,
                v => new ProductName(v));

        builder.Property(x => x.Number)
            .HasConversion(
                v => v.value,
                v => new ProductNumber(v));

        builder.Property(x => x.DefaultPrice)
            .HasConversion(
                v => v.value,
                v => new Price(v));

        builder.Property(x => x.DefaultCurrency)
            .HasConversion(
                v => v.value,
                v => new Currency(v));

        //Relationships
        builder.HasMany(s => s.Stocks);
    }
}