using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalDataWarehouse.Data;
using RentalDataWarehouse.Models;

namespace RentalDataWarehouse.Controllers.CartControllers
{
    [Produces("application/json")]
    [Route("api/Cart")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart([FromRoute] Guid customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cart cart = await _context.Carts.SingleOrDefaultAsync(m => m.Customer.Id == customerId);

            if (cart == null)
            {
                return await newCart(await _context.Customers.SingleOrDefaultAsync(i => i.Id == customerId));
            }

            return Ok(cart);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart([FromRoute] Guid id, [FromBody] CartItem[] cartItems)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (CartItem item in cartItems)
            {
                if (id != item.Cart.Id)
                {
                    return BadRequest(item.Id);
                }
            }

            _context.CartItems.AddRange(cartItems);

            try
            {
                await _context.SaveChangesAsync();

                return Ok(await _context.Carts.SingleOrDefaultAsync(i => i.Id == id));
            }
            catch (DbUpdateConcurrencyException)
            {
                foreach (CartItem item in cartItems)
                {
                    if (!CartItemExists(item.Id))
                    {
                        return NotFound();
                    }
                }

                throw;
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cart cart = await _context.Carts.SingleOrDefaultAsync(i => i.Id == id);

            if (null == cart)
            {
                return NotFound();
            }

            await cart.ClearCart(_context);

            return Ok(await _context.Carts.SingleOrDefaultAsync(i => i.Id == id));
        }


        private bool CartItemExists(Guid id)
        {
            return _context.CartItems.Any(e => e.Id == id);
        }

        private async Task<IActionResult> newCart(Customer customer)
        {
            if (null == customer)
            {
                return NotFound();
            }

            Cart cart = new Cart
            {
                Id = Guid.NewGuid(),
                Customer = customer,
                Items = new CartItem[0]
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCart", new { id = cart.Id }, cart);
        }


    }
}