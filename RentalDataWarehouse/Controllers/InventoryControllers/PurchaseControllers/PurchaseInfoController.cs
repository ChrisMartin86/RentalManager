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
    public class PurchaseInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseInfoController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PurchaseInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.PurchaseItemInfo.ToListAsync());
        }

        // GET: PurchaseInfo/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseInfo = await _context.PurchaseItemInfo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (purchaseInfo == null)
            {
                return NotFound();
            }

            return View(purchaseInfo);
        }

        // GET: PurchaseInfo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PurchaseInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ExternalId")] PurchaseInfo purchaseInfo)
        {
            if (ModelState.IsValid)
            {
                purchaseInfo.Id = Guid.NewGuid();
                _context.Add(purchaseInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(purchaseInfo);
        }

        // GET: PurchaseInfo/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseInfo = await _context.PurchaseItemInfo.SingleOrDefaultAsync(m => m.Id == id);
            if (purchaseInfo == null)
            {
                return NotFound();
            }
            return View(purchaseInfo);
        }

        // POST: PurchaseInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,ExternalId")] PurchaseInfo purchaseInfo)
        {
            if (id != purchaseInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseInfoExists(purchaseInfo.Id))
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
            return View(purchaseInfo);
        }

        // GET: PurchaseInfo/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseInfo = await _context.PurchaseItemInfo
                .SingleOrDefaultAsync(m => m.Id == id);
            if (purchaseInfo == null)
            {
                return NotFound();
            }

            return View(purchaseInfo);
        }

        // POST: PurchaseInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var purchaseInfo = await _context.PurchaseItemInfo.SingleOrDefaultAsync(m => m.Id == id);
            _context.PurchaseItemInfo.Remove(purchaseInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PurchaseInfoExists(Guid id)
        {
            return _context.PurchaseItemInfo.Any(e => e.Id == id);
        }
    }
}
