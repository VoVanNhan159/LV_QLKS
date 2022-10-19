using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareModel;

namespace LV_QLKS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeofaccountsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public TypeofaccountsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Typeofaccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Typeofaccount>>> GetTypeofaccounts()
        {
          if (_context.Typeofaccounts == null)
          {
              return NotFound();
          }
            return await _context.Typeofaccounts.ToListAsync();
        }

        // GET: api/Typeofaccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Typeofaccount>> GetTypeofaccount(int id)
        {
          if (_context.Typeofaccounts == null)
          {
              return NotFound();
          }
            var typeofaccount = await _context.Typeofaccounts.FindAsync(id);

            if (typeofaccount == null)
            {
                return NotFound();
            }

            return typeofaccount;
        }

        // PUT: api/Typeofaccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeofaccount(int id, Typeofaccount typeofaccount)
        {
            if (id != typeofaccount.ToaId)
            {
                return BadRequest();
            }

            _context.Entry(typeofaccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeofaccountExists(id))
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

        // POST: api/Typeofaccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Typeofaccount>> PostTypeofaccount(Typeofaccount typeofaccount)
        {
          if (_context.Typeofaccounts == null)
          {
              return Problem("Entity set 'LV_QLKSContext.Typeofaccounts'  is null.");
          }
            _context.Typeofaccounts.Add(typeofaccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeofaccount", new { id = typeofaccount.ToaId }, typeofaccount);
        }

        // DELETE: api/Typeofaccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeofaccount(int id)
        {
            if (_context.Typeofaccounts == null)
            {
                return NotFound();
            }
            var typeofaccount = await _context.Typeofaccounts.FindAsync(id);
            if (typeofaccount == null)
            {
                return NotFound();
            }

            _context.Typeofaccounts.Remove(typeofaccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeofaccountExists(int id)
        {
            return (_context.Typeofaccounts?.Any(e => e.ToaId == id)).GetValueOrDefault();
        }
    }
}
