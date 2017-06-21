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
        public List<RentalItem> RentalItems { get; set; }
        public List<PurchaseItem> PurchaseItems { get; set; }

        public void ClearCart()
        {
            throw new NotImplementedException();
        }

        public void AddItem(CartItem cartItem, ApplicationDbContext context)
        {
            switch (cartItem.ItemType)
            {
                case CartItemType.Rental:
                    {
                        var item = new RentalItem
                        {

                        };
                        break;
                    }
                case CartItemType.Purchase:
                    {
                        var item = new PurchaseItem
                        {

                        };
                        break;
                    }
                default:
                    {
                        throw new NotImplementedException(nameof(cartItem.ItemType));
                    }
                    
            }
        }
    }
}
