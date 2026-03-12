using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;

namespace WarehouseStorage.Domain.Models
{
    public class Product
    {
        public Guid? Id { get; private set;}
        public ProductName Name { get; private set;}
        public ProductNumber Number { get; private set;}
        public Price DefaultPrice { get; private set;}
        public Currency DefaultCurrency { get; private set;}

        private Product() { } // EF Core

        //Relationsships
        public List<Stock> Stocks {get; set;}

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