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
    public class DiscountdetailsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public DiscountdetailsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Discountdetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discountdetail>>> GetDiscountdetails()
        {
            return await _context.Discountdetails.ToListAsync();
        }
        [HttpGet("GetAllDiscountdetailActive")]
        public async Task<ActionResult<IEnumerable<Discountdetail>>> GetAllDiscountdetailActive()
        {
            var discountDetails = new List<Discountdetail>();
            var discounts = await _context.Discounts.ToListAsync();
            foreach(var item in discounts)
            {
                if(item.DiscountDateend <= DateTime.Now)
                {
                    var discountDetailsTemp = await _context.Discountdetails.Where(dd=>dd.DiscountId == item.DiscountId).ToListAsync();
                    discountDetails.AddRange(discountDetailsTemp);
                }
            }
            return discountDetails;
        }
        [HttpGet("GetAllDiscountdetailActiveDate/{dateStart}/{dateEnd}")]
        public async Task<ActionResult<IEnumerable<Discountdetail>>> GetAllDiscountdetailActiveDate(DateTime dateStart, DateTime dateEnd)
        {
            var discountDetails = new List<Discountdetail>();
            var discounts = await _context.Discounts.ToListAsync();
            foreach (var item in discounts)
            {
                if (item.DiscountDatestart <= dateStart && item.DiscountDateend >= dateEnd)
                {
                    var discountDetailsTemp = await _context.Discountdetails.Where(dd => dd.DiscountId == item.DiscountId).ToListAsync();
                    discountDetails.AddRange(discountDetailsTemp);
                }
            }
            return discountDetails;
        }

        // GET: api/Discountdetails/5
        [HttpGet("{discountId}/{roomId}")]
        public async Task<ActionResult<Discountdetail>> GetDiscountdetail(int discountId, int roomId)
        {
            var discountdetail = await _context.Discountdetails.FirstOrDefaultAsync(dd=>dd.DiscountId == discountId && dd.RoomId == roomId);

            if (discountdetail == null)
            {
                return NotFound();
            }

            return discountdetail;
        }
        

        // PUT: api/Discountdetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{discountId}/{roomId}")]
        public async Task<IActionResult> PutDiscountdetail(int discountId, DiscountDetail_Custom discountdetail)
        {
            if (discountId != discountdetail.DiscountId)
            {
                return BadRequest();
            }
            var discountdetailTemp = new Discountdetail();
            discountdetailTemp.DiscountId = discountdetail.DiscountId;
            discountdetailTemp.RoomId = discountdetail.RoomId;
            discountdetailTemp.Percent = discountdetail.Percent;
            _context.Entry(discountdetailTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountdetailExists(discountId, discountdetail.RoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiscountdetail", new { discountId = discountdetailTemp.DiscountId, roomId = discountdetailTemp.RoomId }, discountdetailTemp);
        }

        // POST: api/Discountdetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiscountDetail_Custom>> PostDiscountdetail(DiscountDetail_Custom discountdetail)
        {
            var discountdetailTemp = new Discountdetail();
            discountdetailTemp.DiscountId = discountdetail.DiscountId;
            discountdetailTemp.RoomId = discountdetail.RoomId;
            discountdetailTemp.Percent = discountdetail.Percent;
            _context.Discountdetails.Add(discountdetailTemp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DiscountdetailExists(discountdetail.DiscountId, discountdetail.RoomId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDiscountdetail", new { discountId = discountdetailTemp.DiscountId, roomId = discountdetailTemp.RoomId }, discountdetailTemp);;
        }

        // DELETE: api/Discountdetails/5
        [HttpDelete("{discountId}/{roomId}")]
        public async Task<IActionResult> DeleteDiscountdetail(int discountId, int roomId)
        {
            var discountdetail = await _context.Discountdetails.FindAsync(discountId, roomId);
            if (discountdetail == null)
            {
                return NotFound();
            }

            _context.Discountdetails.Remove(discountdetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiscountdetailExists(int discountId, int roomId)
        {
            return _context.Discountdetails.Any(e => e.DiscountId == discountId && e.RoomId == roomId);
        }
        [HttpGet("GetAllDiscountdetailOfDiscount/{id}")]
        public async Task<List<Discountdetail>> GetAllDiscountdetailOfDiscount(int id)
        {
            var discountdetail = await _context.Discountdetails.Where(d => d.DiscountId == id).ToListAsync();

            if (discountdetail == null)
            {
                return null;
            }

            return discountdetail;
        }
        [HttpGet("GetAllDiscountdetailOfOwner/{phone}")]
        public async Task<List<Discountdetail>> GetAllDiscountdetailOfOwner(string phone)
        {
            var discouts = await _context.Discounts.Where(d => d.UserPhone == phone).ToListAsync();
            var discountdetail = await _context.Discountdetails.Where(dd => discouts.Select(d=>d.DiscountId).Contains(dd.DiscountId)).ToListAsync();

            if (discountdetail == null)
            {
                return null;
            }

            return discountdetail;
        }
    }
}
