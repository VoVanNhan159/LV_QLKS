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
    public class OrderroomdetailsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public OrderroomdetailsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Orderroomdetails
        [HttpGet("{orderroom_id}")]
        public async Task<List<Orderroomdetail>> GetAllOrderroomdetailOfOrderroom(int orderroom_id)
        {
            return await _context.Orderroomdetails
                .Include(odd=>odd.Orderroom)
                .Include(odd=>odd.Room)
                .Where(od=>od.OrderroomId == orderroom_id).ToListAsync();
        }
        [HttpGet("GetOrderroomdetail/{orderroom_id}")]
        public async Task<Orderroomdetail> GetOrderroomdetail(int orderroom_id)
        {
            return await _context.Orderroomdetails.FindAsync(orderroom_id);
        }

        // PUT: api/Orderroomdetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderroomdetail(int id, Orderroomdetail orderroomdetail)
        {
            if (id != orderroomdetail.RoomId)
            {
                return BadRequest();
            }

            _context.Entry(orderroomdetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderroomdetailExists(id))
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

        // POST: api/Orderroomdetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderroomDetail_Custom>> PostOrderroomdetail(OrderroomDetail_Custom orderroomdetail)
        {
            var orderroomdetailtemp = new Orderroomdetail();
            orderroomdetailtemp.RoomId = orderroomdetail.RoomId;
            orderroomdetailtemp.OrderroomId = orderroomdetail.OrderroomId;
            _context.Orderroomdetails.Add(orderroomdetailtemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderroomdetailExists(orderroomdetailtemp.RoomId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderroomdetail", new { orderroom_id = orderroomdetail.OrderroomId }, orderroomdetailtemp);
        }

        // DELETE: api/Orderroomdetails/5
        [HttpDelete("{orid}/{room_id}")]
        public async Task<IActionResult> DeleteOrderroomdetail(int orid, int room_id)
        {
            var orderroomdetail = await _context.Orderroomdetails.SingleAsync(od=>od.OrderroomId == orid && od.RoomId == room_id);
            if (orderroomdetail == null)
            {
                return NotFound();
            }

            _context.Orderroomdetails.Remove(orderroomdetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderroomdetailExists(int id)
        {
            return _context.Orderroomdetails.Any(e => e.RoomId == id);
        }
    }
}
