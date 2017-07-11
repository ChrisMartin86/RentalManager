using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalDataWarehouse.Data;
using RentalDataWarehouse.Models;
using Microsoft.AspNetCore.Identity;

namespace RentalDataWarehouse.Controllers
{
    [Produces("application/json")]
    [Route("api/CustomerInfo")]
    public class CustomerInfoApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private const string authorizedRoleName = "Administrator";

        public CustomerInfoApiController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/CustomerInfo
        [HttpGet]        
        public async Task<IActionResult> GetCustomers([FromRoute]bool all = false)
        {
            if (null == User)
                return Forbid();

            if (!all && null != User)
            {
                Customer user;

                ApplicationUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

                if (!Customer.tryFind(appUser.Email, _context, out user))
                    return NotFound();
                else
                    return Ok(new Customer[] { user });
            }

            if (!User.IsInRole(authorizedRoleName))
                return Forbid();

            return Ok(_context.Customers);
        }

        // GET: api/CustomerInfo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (null == User)
                return Forbid();

            if (!User.IsInRole(authorizedRoleName))
            {
                Guid currentUserCustomerId = (await _userManager.FindByNameAsync(User.Identity.Name)).CustomerId;

                if (currentUserCustomerId != id)
                    return Forbid();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/CustomerInfoApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] Guid id, [FromBody] Customer customer)
        {
            if (null == User)
                return Forbid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            if (!User.IsInRole(authorizedRoleName))
            {
                Guid currentUserCustomerId = (await _userManager.FindByNameAsync(User.Identity.Name)).CustomerId;

                if (currentUserCustomerId != id)
                    return Forbid();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/CustomerInfoApi
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (null == User)
                return Forbid();

            if (!User.IsInRole(authorizedRoleName))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/CustomerInfoApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            if (null == User)
                return Forbid();

            if (!User.IsInRole(authorizedRoleName))
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}