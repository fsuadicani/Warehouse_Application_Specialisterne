using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.Domain.Models
{
    public class Warehouse
    {
        public Guid? Id { get; }

        public Warehouse(Guid? id)
        {
            Id = id;
        }
    }
}