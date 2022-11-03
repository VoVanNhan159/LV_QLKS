using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class HotelServiceService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/HotelServices";

        public async Task<List<HotelService>> GetAllHotelServiceOfHotel(int id)
        {
            return await Http.GetFromJsonAsync<List<HotelService>>(baseurl + "/" + id);
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
