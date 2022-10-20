using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShareModel;
using ShareModel.Custom;
using ShareModel.Paging;

namespace LV_QLKS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly LV_QLKSContext _context;

        public HotelsController(LV_QLKSContext context)
        {
            _context = context;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotelTemp = _context.Hotels.Include(h=>h.Ward).ToList();

            var hotel = new List<Hotel>();
            foreach (var item in hotelTemp)
            {
                var image = _context.ImageHotels.Where(i => i.HotelId == item.HotelId).FirstOrDefault();
                var hotelAdd = new Hotel();
                hotelAdd = item;
                hotelAdd.ImageHotels.Add(image);
                hotel.Add(hotelAdd);
            }
            return hotel;
        }
        [HttpGet("GetAllHotelOfOwner/{ownerid}")]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetAllHotelOfOwner(string ownerid)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotelTemp = _context.Hotels.Where(h => h.UserPhone == ownerid).ToList();

            var hotel = new List<Hotel>();
            foreach (var item in hotelTemp)
            {
                var image = _context.ImageHotels.Where(i => i.HotelId == item.HotelId).FirstOrDefault();
                var hotelAdd = new Hotel();
                hotelAdd = item;
                hotelAdd.ImageHotels.Add(image);
                hotel.Add(hotelAdd);
            }
            return hotel;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotels
                .Include(h => h.ImageHotels).SingleOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel_Custom hotel)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest();
            }
            var hotelTemp = new Hotel();
            hotelTemp.HotelId = hotel.HotelId;
            hotelTemp.HotelName = hotel.HotelName;
            hotelTemp.UserPhone = hotel.UserPhone;
            hotelTemp.HotelX = hotel.HotelX;
            hotelTemp.HotelY = hotel.HotelY;
            hotelTemp.HotelAddress = hotel.HotelAddress;
            hotelTemp.DistrictId = hotel.DistrictId;
            hotelTemp.WardId = hotel.WardId;
            hotelTemp.ProvinceId = hotel.ProvinceId;
            hotelTemp.HotelStatus = hotel.HotelStatus;
            _context.Entry(hotelTemp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHotel", new { id = hotelTemp.HotelId }, hotelTemp);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel_Custom>> PostHotel(Hotel_Custom hotel)
        {
            var hotelTemp = new Hotel();
            if (_context.Hotels == null)
            {
                return Problem("Entity set 'LV_QLKSContext.Hotels'  is null.");
            }
            try
            {
                hotelTemp.HotelName = hotel.HotelName;
                hotelTemp.UserPhone = hotel.UserPhone;
                hotelTemp.HotelX = hotel.HotelX;
                hotelTemp.HotelY = hotel.HotelY;
                hotelTemp.HotelAddress = hotel.HotelAddress;
                hotelTemp.DistrictId = hotel.DistrictId;
                hotelTemp.WardId = hotel.WardId;
                hotelTemp.ProvinceId = hotel.ProvinceId;
                _context.Hotels.Add(hotelTemp);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return CreatedAtAction("GetHotel", new { id = hotelTemp.HotelId }, hotelTemp);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotels?.Any(e => e.HotelId == id)).GetValueOrDefault();
        }

        //Get: MinPriceOfHotel
        [HttpGet("GetMinPriceHotel/{id}")]
        public async Task<ActionResult<int>> GetMinPriceHotel(int id)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var data = (from r in _context.Rooms
                        join h in _context.Hotels on r.HotelId equals id
                        join t in _context.Typeofrooms on r.TorId equals t.TorId
                        select new
                        {

                            price = t.TorPrice
                        });
            var rs = await Task.FromResult(data.Min(p => p.price));
            return Ok(rs);
        }

        //Get AddressOfHotel
        [HttpGet("GetAddressHotel/{id}")]
        public async Task<ActionResult<string>> GetAddressHotel(int id)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotels.FindAsync(id);
            var pro = await _context.Provinces.Where(p => p.ProvinceId == hotel.ProvinceId).SingleAsync();
            var dis = await _context.Districts.Where(p => p.DistrictId == hotel.DistrictId).SingleAsync();
            var war = await _context.Wards.Where(p => p.WardId == hotel.WardId).SingleAsync();
            var address = hotel.HotelAddress;
            var fulladdress = address + ", " + war.WardName + ", " + dis.DistrictName + ", " + pro.ProvinceName;
            return Ok(fulladdress);
        }

        //Get GetAllRoomOfHotel
        [HttpGet("GetAllRoomOfHotel/{id}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetAllRoomOfHotel(int id)
        {
            if (_context.Hotels == null)
            {
                return NotFound();
            }
            var listRoom = await _context.Rooms.Include(r => r.ImageRooms).Where(r => r.HotelId == id).ToListAsync();
            var newListRoom = new List<Room>();
            foreach (var room in listRoom)
            {
                var tor = _context.Typeofrooms.Find(room.TorId);
                room.Tor = tor;
                newListRoom.Add(room);
            }
            return newListRoom;
        }
        //Get RateOfHotel
        [HttpGet("RateOfHotel/{id}")]
        public string RateOfHotel(int id)
        {
            var hotel = _context.Hotels.Include(h => h.Rooms).Where(h => h.HotelId == id);
            var listRate = _context.Customerreviews.ToList();
            double totalStart = 0;
            int count = 0;
            foreach (var item in listRate)
            {
                foreach (var h in hotel.Select(h => h.Rooms))
                {
                    if (h.Select(hh => hh.RoomId).Contains(item.RoomId))
                    {
                        totalStart += (double)item.CrStar;
                        count++;
                    }
                }
            }
            double res = totalStart / count;
            return res.ToString();
        }
        [HttpGet("CountRoomOfHotel/{id}")]
        public string CountRoomOfHotel(int id)
        {
            var rooms = _context.Rooms.Where(r => r.HotelId == id).ToList();
            return rooms.Count.ToString();
        }

        [HttpGet("PageNumber")]
        public async Task<ActionResult<PagedList<Hotel>>> GetHotelPage([FromQuery] PagingParameters paging)
        {
            var hotel = _context.Hotels.Where(h=>h.UserPhone == paging.phone).AsQueryable();
            var result = PagedList<Hotel>.ToPagedList(hotel, paging.PageNumber, paging.PageSize);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(result.MetaData));
            return result;
        }

        [HttpGet("GetAllHotelIsActive")]
        public async Task<List<Hotel>> GetAllHotelIsActive()
        {
            var hotels = new List<Hotel>();
            var hotelsTemp = await _context.Hotels.Include(h=>h.Ward).ToListAsync();
            var priceListBRs = await _context.Pricelistbrs.ToListAsync();
            var hotel = new List<Hotel>();
            foreach (var item in hotelsTemp)
            {
                var image = _context.ImageHotels.Where(i => i.HotelId == item.HotelId).FirstOrDefault();
                var hotelAdd = new Hotel();
                hotelAdd = item;
                hotelAdd.ImageHotels.Add(image);
                hotel.Add(hotelAdd);
            }

            var businessregistrations = await _context.Businessregistrations.Include(br=>br.Hotel).ToListAsync();
            foreach(var item in businessregistrations)
            {
                var priceListBR = priceListBRs.Single(plbr => plbr.PricelistbrId == item.PricelistbrId);
                if (DateTime.Parse(item.BrDate.ToString()).AddMonths((int)priceListBR.PricelistbrMonth) >= DateTime.Now)
                    hotels.Add(item.Hotel);
            }
            
            return hotels;
        }
        [HttpGet("GetAllHotelIsActiveOfOwner/{phone}")]
        public async Task<List<Hotel>> GetAllHotelIsActiveOfOwner(string phone)
        {
            var hotels = new List<Hotel>();
            var hotelsTemp = await _context.Hotels.Where(h=>h.UserPhone == phone).Include(h=>h.Ward).ToListAsync();
            var priceListBRs = await _context.Pricelistbrs.ToListAsync();
            var hotel = new List<Hotel>();
            foreach (var item in hotelsTemp)
            {
                var image = _context.ImageHotels.Where(i => i.HotelId == item.HotelId).FirstOrDefault();
                var hotelAdd = new Hotel();
                hotelAdd = item;
                hotelAdd.ImageHotels.Add(image);
                hotel.Add(hotelAdd);
            }

            var businessregistrations = await _context.Businessregistrations.Include(br=>br.Hotel).ToListAsync();
            foreach(var item in businessregistrations)
            {
                var priceListBR = priceListBRs.Single(plbr => plbr.PricelistbrId == item.PricelistbrId);
                if (DateTime.Parse(item.BrDate.ToString()).AddMonths((int)priceListBR.PricelistbrMonth) >= DateTime.Now)
                    hotels.Add(item.Hotel);
            }
            
            return hotels;
        }
    }
}
