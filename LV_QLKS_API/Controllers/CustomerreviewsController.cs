using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareModel;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerreviewsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public CustomerreviewsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Customerreviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customerreview>>> GetCustomerreviews()
        {
            return await _context.Customerreviews.ToListAsync();
        }

        // GET: api/Customerreviews/5
        [HttpGet("{id}")]
        public async Task<List<Customerreview>> GetAllCustomerreview(int id)
        {
            var listRoom = await _context.Rooms.Where(r => r.HotelId == id).ToListAsync();
            var customerreview = await _context.Customerreviews.Include(cr=>cr.UserPhoneNavigation).Where(cr=>listRoom.Select(h=>h.RoomId).Contains(cr.RoomId)).ToListAsync();

            if (customerreview == null)
            {
                return null;
            }

            return customerreview;
        }

        // PUT: api/Customerreviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerreview(int id, Customerreview customerreview)
        {
            if (id != customerreview.RoomId)
            {
                return BadRequest();
            }

            _context.Entry(customerreview).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerreviewExists(id))
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

        // POST: api/Customerreviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customerreview>> PostCustomerreview(Customerreview customerreview)
        {
            _context.Customerreviews.Add(customerreview);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerreviewExists(customerreview.RoomId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerreview", new { id = customerreview.RoomId }, customerreview);
        }

        // DELETE: api/Customerreviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerreview(int id)
        {
            var customerreview = await _context.Customerreviews.FindAsync(id);
            if (customerreview == null)
            {
                return NotFound();
            }

            _context.Customerreviews.Remove(customerreview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerreviewExists(int id)
        {
            return _context.Customerreviews.Any(e => e.RoomId == id);
        }
    }
}
