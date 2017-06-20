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
    public class RentalInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalInfoController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: RentalInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.RentalItemInfo.ToListAsync());
        }

        // GET: RentalInfo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalInfo = await _context.RentalItemInfo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (rentalInfo == null)
            {
                return NotFound();
            }

            return View(rentalInfo);
        }

        // GET: RentalInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RentalInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TotalCopies,ExternalId")] RentalInfo rentalInfo)
        {
            if (ModelState.IsValid)
            {
                rentalInfo.Id = Guid.NewGuid();
                _context.Add(rentalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(rentalInfo);
        }

        // GET: RentalInfo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalInfo = await _context.RentalItemInfo.SingleOrDefaultAsync(m => m.Id == id);
            if (rentalInfo == null)
            {
                return NotFound();
            }
            return View(rentalInfo);
        }

        // POST: RentalInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,TotalCopies,ExternalId")] RentalInfo rentalInfo)
        {
            if (id != rentalInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentalInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalInfoExists(rentalInfo.Id))
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
            return View(rentalInfo);
        }

        // GET: RentalInfo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalInfo = await _context.RentalItemInfo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (rentalInfo == null)
            {
                return NotFound();
            }

            return View(rentalInfo);
        }

        // POST: RentalInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rentalInfo = await _context.RentalItemInfo.SingleOrDefaultAsync(m => m.Id == id);
            _context.RentalItemInfo.Remove(rentalInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RentalInfoExists(Guid id)
        {
            return _context.RentalItemInfo.Any(e => e.Id == id);
        }
    }
}
