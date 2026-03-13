using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.DTOs.DataTransferObjects
{
    public class MultipleProductDTO
    {
        public Guid? Id { get; set;}
        required public string Name { get; set;}
        required public string Number { get; set;}
    }
}