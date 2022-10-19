using ShareModel;

namespace LV_QLKS.Service
{
    public class DistrictService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Districts";
        public async Task<List<District>> GetAllDistrictOfProvine(string province_id)
        {
            return await Http.GetFromJsonAsync<List<District>>(baseurl + "/GetAllDistrictOfProvine/" + province_id);
        }
        public async Task<List<District>> GetAllDistrict()
        {
            return await Http.GetFromJsonAsync<List<District>>(baseurl);
        }
        public async Task<District> GetDistrict(string id)
        {
            return await Http.GetFromJsonAsync<District>(baseurl + "/" + id);
        }
    }
}
