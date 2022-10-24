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
    public class OrderroomsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public OrderroomsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Orderrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orderroom>>> GetOrderrooms()
        {
            return await _context.Orderrooms.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Orderroom> GetOrderroom(int id)
        {
            var orderroom = await _context.Orderrooms.Where(od => od.OrderroomId == id)
                .Include(od => od.Orderroomdetails)
                .Include(od => od.UserPhoneNavigation).FirstOrDefaultAsync();

            if (orderroom == null)
            {
                return null;
            }

            return orderroom;
        }

        // GET: api/Orderrooms/5
        [HttpGet("GetAllOrderromOfUser/{phone}")]
        public async Task<List<Orderroom>> GetAllOrderromOfUser(string phone)
        {
            var orderroom = await _context.Orderrooms
                .Include(od=>od.Orderroomdetails)
                .Include(od=>od.UserPhoneNavigation)
                .Where(or=>or.UserPhone == phone).ToListAsync();

            if (orderroom == null)
            {
                return null;
            }

            return orderroom;
        }
        [HttpGet("GetOrderroomById/{id}")]
        public async Task<Orderroom> GetOrderroomById(string id)
        {
            var orderroom = await _context.Orderrooms.Where(od=>od.OrderroomId.ToString() == id).FirstOrDefaultAsync();

            if (orderroom == null)
            {
                return null;
            }

            return orderroom;
        }

        // PUT: api/Orderrooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderroom(int id, [FromBody] Orderroom_Custom orderroom)
        {
            var editOrderroom = await _context.Orderrooms.FindAsync(orderroom.OrderroomId);
            if (id != orderroom.OrderroomId)
            {
                return BadRequest();
            }

           if(editOrderroom == null)
            {
                return NotFound();
            }
            editOrderroom.OrderroomStatus = orderroom.OrderroomStatus;
            editOrderroom.OrderroomTotalprice = orderroom.OrderroomTotalprice;
            editOrderroom.OrderroomDate = orderroom.OrderroomDate;
            editOrderroom.OrderroomDatestart = orderroom.OrderroomDatestart;
            editOrderroom.OrderroomDateend = orderroom.OrderroomDateend;
            editOrderroom.OrderroomStatus = orderroom.OrderroomStatus;
            editOrderroom.OrderroomTotalprice = orderroom.OrderroomTotalprice;
            editOrderroom.UserPhone = orderroom.UserPhone;
            try
            {
                _context.Update(editOrderroom);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderroomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderroom", new { id = editOrderroom.OrderroomId }, editOrderroom);
        }

        // POST: api/Orderrooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Orderroom>> PostOrderroom(Orderroom orderroom)
        {
            try
            {
                orderroom.UserPhoneNavigation = null;
                _context.Orderrooms.Add(orderroom);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return CreatedAtAction("GetOrderroom", new { id = orderroom.OrderroomId }, orderroom);
        }
        [HttpPost("AddOrderroomCustom")]
        public async Task<ActionResult<Orderroom>> AddOrderroomCustom(Orderroom_Custom orderroom)
        {
            var orderroomTemp = new Orderroom();
            try
            {
                orderroomTemp.OrderroomDatestart = orderroom.OrderroomDatestart;
                orderroomTemp.OrderroomDateend = orderroom.OrderroomDateend;
                orderroomTemp.OrderroomDate = orderroom.OrderroomDate;
                orderroomTemp.OrderroomStatus = orderroom.OrderroomStatus;
                orderroomTemp.OrderroomTotalprice = orderroom.OrderroomTotalprice;
                orderroomTemp.UserPhone = orderroom.UserPhone;
                _context.Orderrooms.Add(orderroomTemp);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex.ToString()); 
            }

            return CreatedAtAction("GetOrderroom", new { id = orderroomTemp.OrderroomId }, orderroomTemp);
        }

        // DELETE: api/Orderrooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderroom(int id)
        {
            var orderroom = await _context.Orderrooms.FindAsync(id);
            var listOrderroomDetail = await _context.Orderroomdetails.Where(od => od.OrderroomId == orderroom.OrderroomId).ToListAsync();
            foreach(var orderroomDetail in listOrderroomDetail)
            {
                _context.Remove(orderroomDetail);
            }
            if (orderroom == null)
            {
                return NotFound();
            }

            _context.Orderrooms.Remove(orderroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderroomExists(int id)
        {
            return _context.Orderrooms.Any(e => e.OrderroomId == id);
        }
        [HttpGet("GetAllOrderroomOfOwner/{phone}")]
        public async Task<List<Orderroom>> GetAllOrderroomOfOwner(string phone)
        {
            var orderroomsReturn = new List<Orderroom>();
            var hotels = await _context.Hotels.Where(h => h.UserPhone == phone).ToListAsync();
            var rooms = await _context.Rooms.Where(r => hotels.Select(h => h.HotelId).Contains(r.HotelId)).Select(r=>r.RoomId).ToListAsync();
            var orderrooms = await _context.Orderrooms.Include(o=>o.UserPhoneNavigation).ToListAsync();

            foreach(var item in orderrooms)
            {
                var orderroomdetails = await _context.Orderroomdetails.Include(od=>od.Room).Where(od => od.OrderroomId == item.OrderroomId).ToListAsync();
                foreach (var orderroomdetail in orderroomdetails)
                {
                    if (rooms.Contains(orderroomdetail.RoomId))
                    {
                        if (!orderroomsReturn.Select(or => or.OrderroomId).Contains(item.OrderroomId))
                        {
                            var orderroomTemp = new Orderroom();
                            orderroomTemp = item;
                            orderroomTemp.Orderroomdetails = orderroomdetails;
                            orderroomsReturn.Add(orderroomTemp);
                        }
                    }
                }
            }
            return orderroomsReturn;
        }
    }
}
