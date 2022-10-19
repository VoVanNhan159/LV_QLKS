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
    public class ImageRoomsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public ImageRoomsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/ImageRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageRoom>>> GetImageRooms()
        {
          if (_context.ImageRooms == null)
          {
              return NotFound();
          }
            return await _context.ImageRooms.ToListAsync();
        }

        // GET: api/ImageRooms/5
        [HttpGet("{id}")]
        public async Task<List<ImageRoom>> GetImageRoom(int id)
        {
          if (_context.ImageRooms == null)
          {
              return null;
          }
            var imageRoom = _context.ImageRooms.Where(ir=>ir.RoomId == id).ToList();

            if (imageRoom == null)
            {
                return null;
            }

            return imageRoom;
        }

        // PUT: api/ImageRooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageRoom(int id, ImageRoom imageRoom)
        {
            if (id != imageRoom.ImageId)
            {
                return BadRequest();
            }

            _context.Entry(imageRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageRoomExists(id))
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

        // POST: api/ImageRooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageRoom_Custom>> PostImageRoom(ImageRoom_Custom imageRoom)
        {
          if (_context.ImageRooms == null)
          {
              return Problem("Entity set 'LV_QLKSContext.ImageRooms'  is null.");
          }
            var imageRoomTemp = new ImageRoom();
            imageRoomTemp.ImageId = imageRoom.ImageId;
            imageRoomTemp.RoomId = imageRoom.RoomId;
            _context.ImageRooms.Add(imageRoomTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImageRoomExists(imageRoom.ImageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImageRoom", new { id = imageRoomTemp.ImageId }, imageRoomTemp);
        }

        // DELETE: api/ImageRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageRoom(int id)
        {
            if (_context.ImageRooms == null)
            {
                return NotFound();
            }
            var imageRoom = await _context.ImageRooms.FindAsync(id);
            if (imageRoom == null)
            {
                return NotFound();
            }

            _context.ImageRooms.Remove(imageRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageRoomExists(int id)
        {
            return (_context.ImageRooms?.Any(e => e.ImageId == id)).GetValueOrDefault();
        }
    }
}
