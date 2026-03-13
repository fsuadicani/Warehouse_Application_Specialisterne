using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class StockDTO
    {
        public Guid? Id { get; set;}
        required public string InHouseLocation { get; set;}
        required public int Quantity { get; set;}
        required public decimal LocalPrice { get; set;}
        required public string LocalCurrency { get; set;}
    }
}