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
        public Guid? Id {get; private set;}
        public City City { get; private set;}

        public StreetName Street { get; private set;}
        public StreetNumber StreetNumber { get; private set;}
        public ZipCode ZipCode { get; private set;}

        private Address() { } // EF Core
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