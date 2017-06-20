using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public List<RentalItem> Rentals { get; set; }
        public List<PurchaseItem> Purchases { get; set; }
    }
}
