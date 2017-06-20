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
    [Route("api/RentalInfo")]
    public class RentalInfoApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalInfoApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RentalInfoApi
        [HttpGet]
        public IEnumerable<RentalInfo> GetRentalItemInfo()
        {
            return _context.RentalItemInfo;
        }

        // GET: api/RentalInfoApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalInfo([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rentalInfo = await _context.RentalItemInfo.SingleOrDefaultAsync(m => m.Id == id);

            if (rentalInfo == null)
            {
                return NotFound();
            }

            return Ok(rentalInfo);
        }

        // PUT: api/RentalInfoApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentalInfo([FromRoute] Guid id, [FromBody] RentalInfo rentalInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rentalInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(rentalInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalInfoExists(id))
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

        // POST: api/RentalInfoApi
        [HttpPost]
        public async Task<IActionResult> PostRentalInfo([FromBody] RentalInfo rentalInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RentalItemInfo.Add(rentalInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRentalInfo", new { id = rentalInfo.Id }, rentalInfo);
        }

        // DELETE: api/RentalInfoApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalInfo([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rentalInfo = await _context.RentalItemInfo.SingleOrDefaultAsync(m => m.Id == id);
            if (rentalInfo == null)
            {
                return NotFound();
            }

            _context.RentalItemInfo.Remove(rentalInfo);
            await _context.SaveChangesAsync();

            return Ok(rentalInfo);
        }

        private bool RentalInfoExists(Guid id)
        {
            return _context.RentalItemInfo.Any(e => e.Id == id);
        }
    }
}