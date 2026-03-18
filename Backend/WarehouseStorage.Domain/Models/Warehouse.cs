using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.Domain.Models
{
    public class Warehouse
    {
        public Guid? Id { get; private set; }
        private Warehouse() { } // EF Core

        //Relationships
        // public Address Address { get; set; }

        public Location Location  { get; set; }

        public Warehouse(Guid? id)
        {
            Id = id;
        }
    }
}