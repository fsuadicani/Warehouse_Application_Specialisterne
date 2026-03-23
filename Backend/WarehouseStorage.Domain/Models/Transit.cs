using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Enums;

namespace WarehouseStorage.Domain.Models
{
    public class Transit
    {
        public Guid? Id { get; private set;}
        public TransitNumber TransitNumber { get; private set;}
        public PickUpCode PickUpCode { get; private set;}
        public Coordinates GpsLocation { get; private set;}
        public CompanyName Distributor { get; private set;}
        public DeliveryStatus DeliveryStatus { get; set;}
        private Transit() { } // EF Core

        //Relationsships
        public Guid LocationId {get ; set; }
        public Location Location  { get; set; }

        public Guid DestinationId {get ; set; }

        public Location Destination { get; set; }
        public Guid? OriginId {get ; set; }

        public Location? Origin { get; set; }

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