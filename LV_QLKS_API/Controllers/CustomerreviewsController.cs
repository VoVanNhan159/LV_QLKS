using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareModel;
using ShareModel.Custom;

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
        [HttpGet("{id}/{phone}")]
        public async Task<Customerreview> GetCustomerreview(int id, string phone)
        {
            var customerreview = await _context.Customerreviews.Include(cr=>cr.UserPhoneNavigation).FirstOrDefaultAsync(cr=>cr.RoomId == id && cr.UserPhone == phone);

            if (customerreview == null)
            {
                return null;
            }

            return customerreview;
        }
        [HttpGet("GetAllCustomerreviewOfHotel/{id}")]
        public async Task<List<Customerreview>> GetAllCustomerreviewOfHotel(int id)
        {
            var listRoom = await _context.Rooms.Where(r => r.HotelId == id).ToListAsync();
            var customerreview = await _context.Customerreviews.Include(cr=>cr.UserPhoneNavigation).Where(cr=>listRoom.Select(h=>h.RoomId).Contains(cr.RoomId)).ToListAsync();

            if (customerreview == null)
            {
                return null;
            }

            return customerreview;
        }
        [HttpGet("GetAllCustomerreviewOfRoom/{id}")]
        public async Task<List<Customerreview>> GetAllCustomerreviewOfRoom(int id)
        {
            var customerreview = await _context.Customerreviews.Include(cr=>cr.UserPhoneNavigation).Where(cr=> cr.RoomId == id).ToListAsync();

            if (customerreview == null)
            {
                return null;
            }

            return customerreview;
        }

        // PUT: api/Customerreviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCustomerreview(CustomerReview_Custom customerreview)
        {
            Customerreview customerreviewTemp = new Customerreview();
            customerreviewTemp.RoomId = customerreview.RoomId;
            customerreviewTemp.UserPhone = customerreview.UserPhone;
            customerreviewTemp.CrDate = customerreview.CrDate;
            customerreviewTemp.CrComment = customerreview.CrComment;
            customerreviewTemp.CrStar = customerreview.CrStar;

            _context.Entry(customerreviewTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerreviewExists(customerreviewTemp.RoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomerreview", new { id = customerreviewTemp.RoomId, phone = customerreviewTemp.UserPhone }, customerreviewTemp);
        }

        // POST: api/Customerreviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customerreview>> PostCustomerreview(CustomerReview_Custom customerreview)
        {
            var customerReviewTemp = new Customerreview();
            customerReviewTemp.RoomId = customerreview.RoomId;
            customerReviewTemp.UserPhone = customerreview.UserPhone;
            customerReviewTemp.CrDate = customerreview.CrDate;
            customerReviewTemp.CrStar = customerreview.CrStar;
            customerReviewTemp.CrComment = customerreview.CrComment;
            _context.Customerreviews.Add(customerReviewTemp);
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

            return CreatedAtAction("GetCustomerreview", new { id = customerReviewTemp.RoomId }, customerReviewTemp);
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
