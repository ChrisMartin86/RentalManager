using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse.Models
{
    public class PurchaseItem
    {
        public Guid Id { get; set; }
        public PurchaseInfo ItemInformation { get; set; }
    }
}
