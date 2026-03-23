using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseStorage.Domain.Models
{
    public class Location
    {
        public Guid? Id { get; private set;}

        // public Guid ReferenceId { get; private set; }

        public IList<Stock?> Stocks { get; set; }

        public Address? Address { get; set; }
    }
}