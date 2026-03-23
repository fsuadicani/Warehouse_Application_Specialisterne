using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;

namespace WarehouseStorage.Domain.Models
{
    public class Stock
    {
        public Guid? Id { get; private set;}
        public StockLocation InHouseLocation { get; private set;}
        public Quantity Quantity { get; private set;}
        public Price LocalPrice { get; private set;}
        public Currency LocalCurrency { get; private set;}
        private Stock() { } // EF Core

        //Relationsships
        public Guid LocationId  { get; set;}
        public Location Location { get; set; }

        public Guid ProductId  { get; set;}

        public Product Product { get; set; }

        public Stock(StockLocation location, Quantity quantity, Price price, Currency currency, Guid? id)
        {
            Id = id;
            InHouseLocation = location;
            Quantity = quantity;
            LocalPrice = price;
            LocalCurrency = currency;
        }

        public void ChangeQuantityBy(int delta)
        {
            Quantity.ChangeQuantity(delta);
        }
    }
}