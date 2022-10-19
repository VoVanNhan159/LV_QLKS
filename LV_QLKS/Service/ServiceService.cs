
namespace LV_QLKS.Service
{
    public class ServiceService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Services";

        public async Task<List<ShareModel.Service>> GetAllServiceOfHotel(int hotelId)
        {
            return await Http.GetFromJsonAsync<List<ShareModel.Service>>(baseurl + "/" + hotelId);
        }
    }
}
