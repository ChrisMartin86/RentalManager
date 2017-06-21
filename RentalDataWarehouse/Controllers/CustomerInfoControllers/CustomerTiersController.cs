using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalDataWarehouse.Data;
using RentalDataWarehouse.Models;

namespace RentalDataWarehouse.Controllers
{
    public class CustomerTiersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerTiersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: CustomerTiers
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerTier.ToListAsync());
        }

        // GET: CustomerTiers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTier = await _context.CustomerTier
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customerTier == null)
            {
                return NotFound();
            }

            return View(customerTier);
        }

        // GET: CustomerTiers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerTiers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CustomerTier customerTier)
        {
            if (ModelState.IsValid)
            {
                customerTier.Id = Guid.NewGuid();
                _context.Add(customerTier);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerTier);
        }

        // GET: CustomerTiers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTier = await _context.CustomerTier.SingleOrDefaultAsync(m => m.Id == id);
            if (customerTier == null)
            {
                return NotFound();
            }
            return View(customerTier);
        }

        // POST: CustomerTiers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] CustomerTier customerTier)
        {
            if (id != customerTier.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerTier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerTierExists(customerTier.Id))
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
            return View(customerTier);
        }

        // GET: CustomerTiers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerTier = await _context.CustomerTier
                .SingleOrDefaultAsync(m => m.Id == id);
            if (customerTier == null)
            {
                return NotFound();
            }

            return View(customerTier);
        }

        // POST: CustomerTiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var customerTier = await _context.CustomerTier.SingleOrDefaultAsync(m => m.Id == id);
            _context.CustomerTier.Remove(customerTier);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CustomerTierExists(Guid id)
        {
            return _context.CustomerTier.Any(e => e.Id == id);
        }
    }
}
