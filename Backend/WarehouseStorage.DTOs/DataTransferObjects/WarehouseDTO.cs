using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class WarehouseDTO
    {
        public Guid? Id {get; set;}
        public string City { get; set;}

        public string Street { get; set;}
        public string StreetNumber { get; set;}
        public string ZipCode { get; set;}
    }
}