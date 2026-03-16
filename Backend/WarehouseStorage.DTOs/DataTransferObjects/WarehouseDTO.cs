using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class WarehouseDTO
    {
        public Guid? Id {get; set;}
        required public string City { get; set;}
        required public string Street { get; set;}
        required public string StreetNumber { get; set;}
        required public string ZipCode { get; set;}
    }
}