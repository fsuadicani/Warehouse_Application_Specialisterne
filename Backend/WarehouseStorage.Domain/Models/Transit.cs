using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Enums;

namespace WarehouseStorage.Domain.Models
{
    public class Transit
    {
        public Guid? Id { get; }
        public TransitNumber TransitNumber { get; }
        public PickUpCode PickUpCode { get; }
        public Coordinates GpsLocation { get; }
        public CompanyName Distributor { get; }
        public DeliveryStatus DeliveryStatus { get; }

        public Transit(TransitNumber transitNumber, PickUpCode pickUpCode, Coordinates coordinates, CompanyName companyName, DeliveryStatus deliveryStatus, Guid? id)
        {
            Id = id;
            TransitNumber = transitNumber;
            PickUpCode = pickUpCode;
            GpsLocation = coordinates;
            Distributor = companyName;
            DeliveryStatus = deliveryStatus;
        }
    }
}