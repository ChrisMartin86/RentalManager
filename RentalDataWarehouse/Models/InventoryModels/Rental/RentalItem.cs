using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse.Models
{
    public class RentalItem
    {
        public Guid Id { get; set; }
        public RentalInfo ItemInformation { get; set; }
    }
}
