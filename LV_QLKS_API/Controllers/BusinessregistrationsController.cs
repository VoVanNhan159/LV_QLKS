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
    public class BusinessregistrationsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public BusinessregistrationsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Businessregistrations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Businessregistration>>> GetBusinessregistrations()
        {
            return await _context.Businessregistrations.Include(br=>br.Pricelistbr).ToListAsync();
        }

        // GET: api/Businessregistrations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Businessregistration>> GetBusinessregistration(int id)
        {
            var businessregistration = await _context.Businessregistrations.FindAsync(id);

            if (businessregistration == null)
            {
                return NotFound();
            }

            return businessregistration;
        }

        // PUT: api/Businessregistrations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBusinessregistration(int id, Businessregistration businessregistration)
        {
            if (id != businessregistration.BrId)
            {
                return BadRequest();
            }

            _context.Entry(businessregistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BusinessregistrationExists(id))
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

        // POST: api/Businessregistrations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Businessregistration_Custom>> PostBusinessregistration(Businessregistration_Custom businessregistration)
        {
            var brTemp = new Businessregistration();
            brTemp.HotelId = businessregistration.HotelId;
            brTemp.PricelistbrId = businessregistration.PricelistbrId;
            brTemp.BrDate = businessregistration.BrDate;
            brTemp.UserPhone = businessregistration.UserPhone;
            brTemp.BrStatus = businessregistration.BrStatus;
            _context.Businessregistrations.Add(brTemp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBusinessregistration", new { id = brTemp.BrId }, brTemp);
        }

        // DELETE: api/Businessregistrations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBusinessregistration(int id)
        {
            var businessregistration = await _context.Businessregistrations.FindAsync(id);
            if (businessregistration == null)
            {
                return NotFound();
            }

            _context.Businessregistrations.Remove(businessregistration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BusinessregistrationExists(int id)
        {
            return _context.Businessregistrations.Any(e => e.BrId == id);
        }

        [HttpGet("GetAllBusinessregistrationOfOwner/{phone}")]
        public async Task<List<Businessregistration>> GetAllBusinessregistrationOfOwner(string phone)
        {
            var businessregistration = await _context.Businessregistrations
                .Where(br=>br.UserPhone == phone).ToListAsync();

            if (businessregistration == null)
            {
                return null;
            }

            return businessregistration;
        }
    }
}
