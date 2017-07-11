using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalDataWarehouse.Data;

namespace RentalDataWarehouse.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<CartItem> Items { get; set; }

        public async Task ClearCart(ApplicationDbContext context)
        {
            var taskList = new List<Task>();

            foreach (CartItem item in Items)
            {
                context.Remove(item);
            }

            await context.SaveChangesAsync();

            Items = new CartItem[0];
        }

        public async Task AddItem(Guid itemInfoId, ApplicationDbContext context)
        {
            var item = new CartItem
            {
                Id = Guid.NewGuid(),
                Cart = this,
                ItemInfo = context.ItemInfo.Single(i => i.Id == itemInfoId)
            };

            context.Add(item);

            await context.SaveChangesAsync();

            Items = context.CartItems.Where(i => i.Cart == this);
        }
    }
}
