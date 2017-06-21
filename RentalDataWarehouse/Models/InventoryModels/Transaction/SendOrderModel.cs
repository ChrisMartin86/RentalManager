using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse.Models
{
    public class SendOrderModel
    {
        public List<Guid> RentalItems { get; set; }
        public List<Guid> PurchaseItems { get; set; }
        public DateTime PickupDateTime { get; set; }
    }
}
