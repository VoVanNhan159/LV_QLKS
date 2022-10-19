using ShareModel;

namespace LV_QLKS.Service
{
    public class PricelistbrService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Pricelistbrs";
        public async Task<Pricelistbr> GetPricelistbr(int id)
        {
            return await Http.GetFromJsonAsync<Pricelistbr>(baseurl + "/" + id);
        }
        //Lấy tất cả Businessregistration
        public async Task<List<Pricelistbr>> GetAllPricelistbr()
        {
            return await Http.GetFromJsonAsync<List<Pricelistbr>>(baseurl);
        }
    }
}
