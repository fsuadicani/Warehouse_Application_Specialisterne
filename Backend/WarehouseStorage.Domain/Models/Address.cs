using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WarehouseStorage.Domain.DomainPrimitives;

namespace WarehouseStorage.Domain.Models
{
    public class Address
    {
        public Guid? Id {get; }
        public City City { get; }

        public StreetName Street { get; }
        public StreetNumber StreetNumber { get; }
        public ZipCode ZipCode { get; }

        public Address(City city, StreetName streetName, StreetNumber streetNumber, ZipCode zipCode, Guid? guid)
        {
            Id = guid;
            City = city;
            Street = streetName;
            StreetNumber = streetNumber;
            ZipCode = zipCode;
        }
    }
}