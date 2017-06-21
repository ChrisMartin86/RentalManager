using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalDataWarehouse.Models
{
    public class CartItem
    {
        public Guid ItemInfoId { get; set; }
        public CartItemType ItemType { get; set; }

        public async Task<RentalItem> CreateRentalItemAsync(Data.ApplicationDbContext context)
        {
            var item = new RentalItem
            {
                Id = Guid.NewGuid(),
                ItemInformation = context.RentalItemInfo.SingleOrDefault(i => i.Id == ItemInfoId)
            };

            context.Add(item);

            await context.SaveChangesAsync();

            return item;
        }

        public async Task<PurchaseItem> CreatePurchaseItemAsync(Data.ApplicationDbContext context)
        {
            var item = new PurchaseItem
            {
                Id = Guid.NewGuid(),
                ItemInformation = context.PurchaseItemInfo.SingleOrDefault(i => i.Id == ItemInfoId)
            };

            context.Add(item);

            await context.SaveChangesAsync();

            return item;
        }

    }

    public enum CartItemType
    {
        Rental = 0,
        Purchase = 1
    }
}
