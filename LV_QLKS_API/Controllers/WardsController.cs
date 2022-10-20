using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareModel;

namespace BlazorApp1.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public WardsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Wards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetWards()
        {
            return await _context.Wards
                .Include(w=>w.Hotels)
                .ToListAsync();
        }

        // GET: api/Wards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ward>> GetWard(string id)
        {
            var ward = await _context.Wards.Include(w => w.Hotels).FirstAsync(w=>w.WardId == id);

            if (ward == null)
            {
                return NotFound();
            }

            return ward;
        }

        [HttpGet("GetAllWardOfDistrict/{id}")]
        public async Task<List<Ward>> GetAllWardOfDistrict(string id)
        {
            var ward = await _context.Wards.Where(w => w.DistrictId == id).OrderBy(w=>w.WardName).ToListAsync();

            if (ward == null)
            {
                return null;
            }

            return ward;
        }

        // PUT: api/Wards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWard(string id, Ward ward)
        {
            if (id != ward.ProvinceId)
            {
                return BadRequest();
            }

            _context.Entry(ward).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WardExists(id))
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

        // POST: api/Wards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ward>> PostWard(Ward ward)
        {
            _context.Wards.Add(ward);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WardExists(ward.ProvinceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWard", new { id = ward.ProvinceId }, ward);
        }

        // DELETE: api/Wards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWard(string id)
        {
            var ward = await _context.Wards.FindAsync(id);
            if (ward == null)
            {
                return NotFound();
            }

            _context.Wards.Remove(ward);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WardExists(string id)
        {
            return _context.Wards.Any(e => e.ProvinceId == id);
        }
    }
}
