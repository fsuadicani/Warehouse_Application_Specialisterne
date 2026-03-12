using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class ProductDTO
    {
        public Guid? Id { get; set;}
        public string Name { get; set;}
        public string Number { get; set;}
        public decimal DefaultPrice { get; set;}
        public string DefaultCurrency { get; set;}
    }
}