using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalDataWarehouse.Data;
using RentalDataWarehouse.Models;

namespace RentalDataWarehouse.Controllers
{
    public class CustomerInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private const string authorizedRoleName = "Administrator";

        public CustomerInfoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CustomerInfo
        public async Task<IActionResult> Index()
        {


            return View(await _context.Customers.ToListAsync());
        }

        // GET: CustomerInfo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.IsInRole(authorizedRoleName))
            {
                Guid currentUserCustomerId = (await _userManager.FindByNameAsync(User.Identity.Name)).CustomerId;

                if (currentUserCustomerId != id)
                    return Forbid();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: CustomerInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EmailAddress")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Id = Guid.NewGuid();
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: CustomerInfo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: CustomerInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (!User.IsInRole(authorizedRoleName))
            {
                Guid currentUserCustomerId = (await _userManager.FindByNameAsync(User.Identity.Name)).CustomerId;

                if (currentUserCustomerId != id)
                    return new ForbidResult();
            }            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: CustomerInfo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!User.IsInRole(authorizedRoleName))
                return new ForbidResult();

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomerInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (!User.IsInRole(authorizedRoleName))
                return new ForbidResult();

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
