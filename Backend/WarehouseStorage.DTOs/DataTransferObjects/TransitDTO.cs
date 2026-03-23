using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class TransitDTO
    {
        public Guid? Id { get; set;}
        required public string TransitNumber { get; set;}
        required public string PickUpCode { get; set;}
        required public string GpsLocation { get; set;}
        required public string Distributor { get; set;}
        required public string DeliveryStatus { get; set;}

        // required public Guid LocationId {get ; set; }

        required public Guid DestinationId {get ; set; }

        public Guid? OriginId {get ; set; }

        required public ICollection<StockDTO> Stocks {get; set;}
    }
}