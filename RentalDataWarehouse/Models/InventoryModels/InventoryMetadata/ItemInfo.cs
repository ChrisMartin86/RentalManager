using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse.Models
{
    public class ItemInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public ItemType ItemType { get; set; }
    }
}
