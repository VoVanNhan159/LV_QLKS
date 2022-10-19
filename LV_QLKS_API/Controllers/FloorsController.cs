using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public FloorsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Floors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floor>>> GetFloors()
        {
            return await _context.Floors.ToListAsync();
        }

        // GET: api/Floors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Floor>> GetFloor(int id)
        {
            var floor = await _context.Floors.FindAsync(id);

            if (floor == null)
            {
                return NotFound();
            }

            return floor;
        }


        // PUT: api/Floors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFloor(int id, Floor floor)
        {
            if (id != floor.FloorId)
            {
                return BadRequest();
            }

            _context.Entry(floor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FloorExists(id))
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

        // POST: api/Floors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Floor_Custom>> PostFloor(Floor_Custom floor)
        {
            var floorTemp = new Floor();
            floorTemp.FloorName = floor.FloorName;
            floorTemp.FloorDescription = floor.FloorDescription;
            floorTemp.HotelId = floor.HotelId;

            _context.Floors.Add(floorTemp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFloor", new { id = floorTemp.FloorId }, floorTemp);
        }

        // DELETE: api/Floors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFloor(int id)
        {
            var floor = await _context.Floors.FindAsync(id);
            if (floor == null)
            {
                return NotFound();
            }

            _context.Floors.Remove(floor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FloorExists(int id)
        {
            return _context.Floors.Any(e => e.FloorId == id);
        }
        [HttpGet("GetAllFloorOfHotel/{id}")]
        public async Task<List<Floor>> GetAllFloorOfHotel(int id)
        {
            var floor = await _context.Floors.Where(f=>f.HotelId == id).ToListAsync();

            if (floor == null)
            {
                return null;
            }

            return floor;
        }
        [HttpGet("GetAllFloorOfOwner/{phone}")]
        public async Task<List<Floor>> GetAllFloorOfOwner(string phone)
        {
            var hotels = await _context.Hotels.Where(h => h.UserPhone == phone).ToListAsync();
            var floorTemp = await _context.Floors.ToListAsync();
            var floor = new List<Floor>();
            foreach(var item in floorTemp)
            {
                foreach (var h in hotels)
                    if (h.HotelId == item.HotelId)
                    {
                        floor.Add(item);
                        break;
                    } 
            }
            if (floor == null)
            {
                return null;
            }

            return floor;
        }
        [HttpGet("GetAllRoomOfFloor/{id}")]
        public async Task<List<Room>> GetAllRoomOfFloor(int id)
        {
            var room = await _context.Rooms.Where(r => r.FloorId == id).ToListAsync();
            return room;
        }
    }
}
