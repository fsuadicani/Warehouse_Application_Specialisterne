using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class StockDTO
    {
        public Guid? Id { get; set;}
        public string InHouseLocation { get; set;}
        public int Quantity { get; set;}
        public decimal LocalPrice { get; set;}
        public string LocalCurrency { get; set;}
    }
}