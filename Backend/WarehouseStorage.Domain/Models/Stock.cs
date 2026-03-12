using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;

namespace WarehouseStorage.Domain.Models
{
    public class Stock
    {
        public Guid? Id { get; }
        public StockLocation InHouseLocation { get; }
        public Quantity Quantity { get; }
        public Price LocalPrice { get; }
        public Currency LocalCurrency { get; }

        public Stock(StockLocation location, Quantity quantity, Price price, Currency currency, Guid? id)
        {
            Id = id;
            InHouseLocation = location;
            Quantity = quantity;
            LocalPrice = price;
            LocalCurrency = currency;
        }
    }
}