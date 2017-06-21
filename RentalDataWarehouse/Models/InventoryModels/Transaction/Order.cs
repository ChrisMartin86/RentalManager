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
        public bool TransactionComplete { get; set; }
        public DateTime? DateTimeOfTransaction { get; set; }
        public decimal? Total { get; set; }
    }

    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(Guid id) : base($"Order {id} was not found")
        {

        }
    }
}
