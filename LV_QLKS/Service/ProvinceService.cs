using ShareModel;

namespace LV_QLKS.Service
{
    public class ProvinceService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Provinces";
        public async Task<List<Province>> GetAllProvine()
        {
            return await Http.GetFromJsonAsync<List<Province>>(baseurl);
        }
        public async Task<Province> GetProvine(string id)
        {
            return await Http.GetFromJsonAsync<Province>(baseurl + "/" + id);
        }
    }
}
