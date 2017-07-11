using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Cart Cart { get; set; }
        public ItemInfo ItemInfo { get; set; }
    }
}
