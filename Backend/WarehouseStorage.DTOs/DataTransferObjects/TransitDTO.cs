using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class TransitDTO
    {
        public Guid? Id { get; set;}
        public string TransitNumber { get; set;}
        public string PickUpCode { get; set;}
        public string GpsLocation { get; set;}
        public string Distributor { get; set;}
        public string DeliveryStatus { get; set;}

        public ICollection<StockDTO> Stocks {get; set;}
    }
}