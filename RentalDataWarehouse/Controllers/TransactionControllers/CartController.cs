using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalDataWarehouse.Data;
using RentalDataWarehouse.Models;
using Microsoft.AspNetCore.Identity;

namespace RentalDataWarehouse.Controllers.TransactionControllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            Cart myCart = _context.Carts.SingleOrDefault(i => i.Customer.Id == user.CustomerId);

            if (null == myCart)
            {
                var newCart = new Cart
                {
                    Id = Guid.NewGuid(),
                    Customer = _context.Customers.SingleOrDefault(i => i.Id == user.CustomerId)
                };

                _context.Add(newCart);

                await _context.SaveChangesAsync();

                myCart = newCart;
            }

            return View(myCart);
        }

        // POST: Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}