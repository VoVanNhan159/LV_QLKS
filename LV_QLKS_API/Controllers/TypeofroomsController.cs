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
    public class TypeofroomsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public TypeofroomsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Typeofrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Typeofroom>>> GetTypeofrooms()
        {
          if (_context.Typeofrooms == null)
          {
              return NotFound();
          }
            return await _context.Typeofrooms.ToListAsync();
        }

        // GET: api/Typeofrooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Typeofroom>> GetTypeofroom(int id)
        {
          if (_context.Typeofrooms == null)
          {
              return NotFound();
          }
            var typeofroom = await _context.Typeofrooms.FindAsync(id);

            if (typeofroom == null)
            {
                return NotFound();
            }

            return typeofroom;
        }
        [HttpGet("GetAllTypeofroomOfOwner/{phone}")]
        public async Task<List<Typeofroom>> GetAllTypeofroomOfOwner(string phone)
        {
            if (_context.Typeofrooms == null)
            {
                return null;
            }
            var typeofroom = await _context.Typeofrooms.Where(tor=>tor.UserPhone == phone).ToListAsync();

            if (typeofroom == null)
            {
                return null;
            }

            return typeofroom;
        }

        // PUT: api/Typeofrooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeofroom(int id, Typeofroom_Custom typeofroom)
        {
            if (id != typeofroom.TorId)
            {
                return BadRequest();
            }
            var typeofroomTemp = new Typeofroom();
            typeofroomTemp.TorId = typeofroom.TorId;
            typeofroomTemp.TorName = typeofroom.TorName;
            typeofroomTemp.TorCapacity = typeofroom.TorCapacity;
            typeofroomTemp.TorPrice = typeofroom.TorPrice;
            typeofroomTemp.UserPhone = typeofroom.UserPhone;
            _context.Entry(typeofroomTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeofroomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTypeofroom", new { id = typeofroomTemp.TorId }, typeofroomTemp);
        }

        // POST: api/Typeofrooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Typeofroom_Custom>> PostTypeofroom(Typeofroom_Custom typeofroom)
        {
          if (_context.Typeofrooms == null)
          {
              return Problem("Entity set 'LV_QLKSContext.Typeofrooms'  is null.");
          }
            try
            {
                var torTemp = new Typeofroom();
                torTemp.TorCapacity = typeofroom.TorCapacity;
                torTemp.TorPrice = typeofroom.TorPrice;
                torTemp.TorName = typeofroom.TorName;
                torTemp.UserPhone = typeofroom.UserPhone;
                _context.Typeofrooms.Add(torTemp);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTypeofroom", new { id = torTemp.TorId }, torTemp);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());            
            }
            return null;
        }

        // DELETE: api/Typeofrooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeofroom(int id)
        {
            if (_context.Typeofrooms == null)
            {
                return NotFound();
            }
            var typeofroom = await _context.Typeofrooms.FindAsync(id);
            if (typeofroom == null)
            {
                return NotFound();
            }

            _context.Typeofrooms.Remove(typeofroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeofroomExists(int id)
        {
            return (_context.Typeofrooms?.Any(e => e.TorId == id)).GetValueOrDefault();
        }
    }
}
