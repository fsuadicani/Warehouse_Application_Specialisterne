using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class MultipleProductDTO
    {
        public Guid? Id { get; set;}
        public string Name { get; set;}
        public string Number { get; set;}
    }
}