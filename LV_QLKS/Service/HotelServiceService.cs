using Microsoft.EntityFrameworkCore;
using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class HotelServiceService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/HotelServices";

        public async Task<List<HotelServiceCs>> GetAllHotelServiceOfHotel(int id)
        {
            LV_QLKSContext _context = new LV_QLKSContext();
            try
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
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            return null;
        }
        public async Task<HotelServiceCs> AddHotelService(HotelService_Custom hotelService_Custom)
        {
            try
            {
                var response = await Http.PostAsJsonAsync(baseurl + "/", hotelService_Custom);
                return await response.Content.ReadFromJsonAsync<HotelServiceCs>();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
    }
}
