using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalDataWarehouse.Data;

namespace RentalDataWarehouse.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public IEnumerable<Order> Orders { get; set; }

        internal static bool tryFind(string emailAddress, ApplicationDbContext applicationDbContext, out Customer customer)
        {
            customer = null;

            Customer _customer = applicationDbContext.Customers.SingleOrDefault(i => i.EmailAddress == emailAddress);

            if (_customer != null)
            {
                customer = _customer;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
