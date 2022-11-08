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
    public class HotelServiceCssController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public HotelServiceCssController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/HotelServiceCss
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelServiceCs>>> GetHotelServiceCss()
        {
            return await _context.HotelServiceCss.ToListAsync();
        }

        // GET: api/HotelServiceCss/5
        [HttpGet("{id}/{id2}")]
        public async Task<HotelServiceCs> GetHotelService(int id, int id2)
        {
            var hotelService = await _context.HotelServiceCss.FirstOrDefaultAsync(hs => hs.HotelId == id && hs.ServiceId == id2);

            if (hotelService == null)
            {
                return null;
            }

            return hotelService;
        }
        [HttpGet("GetAllHotelServiceOfHotel/{id}")]
        public async Task<ActionResult<List<HotelServiceCs>>> GetAllHotelServiceOfHotel(int id)
        {
            if (_context.Services == null)
            {
                return null;
            }
            var service = await _context.HotelServiceCss.Where(s => s.HotelId == id).ToListAsync();

            if (service == null)
            {
                return null;
            }

            return service;
        }
        // PUT: api/HotelServiceCss/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelService(int id, HotelServiceCs hotelService)
        {
            if (id != hotelService.HotelId)
            {
                return BadRequest();
            }

            _context.Entry(hotelService).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelServiceExists(id))
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

        // POST: api/HotelServiceCss
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HotelServiceCs>> PostHotelService(HotelService_Custom hotelService)
        {

            HotelServiceCs hotelServiceCs = new HotelServiceCs();
            hotelServiceCs.ServiceId = hotelService.ServiceId;
            hotelServiceCs.HotelId = hotelService.HotelId;
            _context.HotelServiceCss.Add(hotelServiceCs);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HotelServiceExists(hotelService.HotelId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            try
            {
                return CreatedAtAction("GetHotelService", new { id = hotelServiceCs.HotelId, id2 = hotelServiceCs.ServiceId }, hotelServiceCs);
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return null;
        }

        // DELETE: api/HotelServiceCss/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelService(int id)
        {
            var hotelService = await _context.HotelServiceCss.FindAsync(id);
            if (hotelService == null)
            {
                return NotFound();
            }

            _context.HotelServiceCss.Remove(hotelService);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("DeleteAllHotelServiceOfHotel/{id}")]
        public async Task<IActionResult> DeleteAllHotelServiceOfHotel(int id)
        {
            List<HotelServiceCs> hotelServiceCsTemp = new List<HotelServiceCs>();
            hotelServiceCsTemp = GetAllHotelServiceOfHotel(id).Result.Value.ToList();
            _context.HotelServiceCss.RemoveRange(hotelServiceCsTemp);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelServiceExists(int id)
        {
            return _context.HotelServiceCss.Any(e => e.HotelId == id);
        }
    }
}
