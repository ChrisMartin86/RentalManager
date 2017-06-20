using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalDataWarehouse.Data;
using RentalDataWarehouse.Models;

namespace RentalDataWarehouse.Controllers
{
    [Produces("application/json")]
    [Route("api/PurchaseInfo")]
    public class PurchaseInfoApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseInfoApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseInfoApi
        [HttpGet]
        public IEnumerable<PurchaseInfo> GetPurchaseItemInfo()
        {
            return _context.PurchaseItemInfo;
        }

        // GET: api/PurchaseInfoApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseInfo([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseInfo = await _context.PurchaseItemInfo.SingleOrDefaultAsync(m => m.Id == id);

            if (purchaseInfo == null)
            {
                return NotFound();
            }

            return Ok(purchaseInfo);
        }

        // PUT: api/PurchaseInfoApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseInfo([FromRoute] Guid id, [FromBody] PurchaseInfo purchaseInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(purchaseInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PurchaseInfoApi
        [HttpPost]
        public async Task<IActionResult> PostPurchaseInfo([FromBody] PurchaseInfo purchaseInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PurchaseItemInfo.Add(purchaseInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseInfo", new { id = purchaseInfo.Id }, purchaseInfo);
        }

        // DELETE: api/PurchaseInfoApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseInfo([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseInfo = await _context.PurchaseItemInfo.SingleOrDefaultAsync(m => m.Id == id);
            if (purchaseInfo == null)
            {
                return NotFound();
            }

            _context.PurchaseItemInfo.Remove(purchaseInfo);
            await _context.SaveChangesAsync();

            return Ok(purchaseInfo);
        }

        private bool PurchaseInfoExists(Guid id)
        {
            return _context.PurchaseItemInfo.Any(e => e.Id == id);
        }
    }
}