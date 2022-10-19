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
    public class ImageHotelsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public ImageHotelsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/ImageHotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageHotel>>> GetImageHotels()
        {
          if (_context.ImageHotels == null)
          {
              return NotFound();
          }
            return await _context.ImageHotels.ToListAsync();
        }

        // GET: api/ImageHotels/5
        [HttpGet("{id}")]
        public async Task<ImageHotel> GetImageHotel(int id)
        {
          if (_context.ImageHotels == null)
          {
              return null;
          }
            var imageHotel = await _context.ImageHotels.FindAsync(id);

            if (imageHotel == null)
            {
                return null;
            }

            return imageHotel;
        }
        // GET: api/ImageHotels/5
        [HttpGet("GetImageOfHotel/{id}")]
        public async Task<List<ImageHotel>> GetImageOfHotel(int id)
        {
            if (_context.ImageHotels == null)
            {
                return null;
            }
            var imageHotel = _context.ImageHotels.Where(ih => ih.HotelId == id).ToList();

            if (imageHotel == null)
            {
                return null;
            }

            return imageHotel;
        }

        // PUT: api/ImageHotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageHotel(int id, ImageHotel imageHotel)
        {
            if (id != imageHotel.ImageId)
            {
                return BadRequest();
            }

            _context.Entry(imageHotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageHotelExists(id))
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

        // POST: api/ImageHotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageHotel_Custom>> PostImageHotel(ImageHotel_Custom imageHotel)
        {
          if (_context.ImageHotels == null)
          {
              return Problem("Entity set 'LV_QLKSContext.ImageHotels'  is null.");
          }
            var imageTemp = new ImageHotel();
            imageTemp.HotelId = imageHotel.HotelId;
            imageTemp.ImageId = imageHotel.ImageId;
            _context.ImageHotels.Add(imageTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImageHotelExists(imageHotel.ImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImageHotel", new { id = imageTemp.ImageId }, imageTemp);
        }

        // DELETE: api/ImageHotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageHotel(int id)
        {
            if (_context.ImageHotels == null)
            {
                return NotFound();
            }
            var imageHotel = await _context.ImageHotels.FindAsync(id);
            if (imageHotel == null)
            {
                return NotFound();
            }

            _context.ImageHotels.Remove(imageHotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageHotelExists(int id)
        {
            return (_context.ImageHotels?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
