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
    [Route("api/CustomerTier")]
    public class CustomerTierApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerTierApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CustomerTierApi
        [HttpGet]
        public IEnumerable<CustomerTier> GetCustomerTier()
        {
            return _context.CustomerTier;
        }

        // GET: api/CustomerTierApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerTier([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerTier = await _context.CustomerTier.SingleOrDefaultAsync(m => m.Id == id);

            if (customerTier == null)
            {
                return NotFound();
            }

            return Ok(customerTier);
        }

        // PUT: api/CustomerTierApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerTier([FromRoute] Guid id, [FromBody] CustomerTier customerTier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerTier.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerTier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerTierExists(id))
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

        // POST: api/CustomerTierApi
        [HttpPost]
        public async Task<IActionResult> PostCustomerTier([FromBody] CustomerTier customerTier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CustomerTier.Add(customerTier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerTier", new { id = customerTier.Id }, customerTier);
        }

        // DELETE: api/CustomerTierApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerTier([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerTier = await _context.CustomerTier.SingleOrDefaultAsync(m => m.Id == id);
            if (customerTier == null)
            {
                return NotFound();
            }

            _context.CustomerTier.Remove(customerTier);
            await _context.SaveChangesAsync();

            return Ok(customerTier);
        }

        private bool CustomerTierExists(Guid id)
        {
            return _context.CustomerTier.Any(e => e.Id == id);
        }
    }
}