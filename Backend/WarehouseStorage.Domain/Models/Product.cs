using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;

namespace WarehouseStorage.Domain.Models
{
    public class Product
    {
        public Guid? Id { get; }
        public ProductName Name { get; }
        public ProductNumber Number { get; }
        public Price DefaultPrice { get; }
        public Currency DefaultCurrency { get; }

        public Product(ProductName name, ProductNumber number, Price price, Currency currency, Guid? id)
        {
            Id = id;
            Name = name;
            Number = number;
            DefaultPrice = price;
            DefaultCurrency = currency;
        }
    }
}