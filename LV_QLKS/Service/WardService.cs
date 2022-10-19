using ShareModel;

namespace LV_QLKS.Service
{
    public class WardService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Wards";
        public async Task<List<Ward>> GetAllWard()
        {
            return await Http.GetFromJsonAsync<List<Ward>>(baseurl);
        }
        public async Task<List<Ward>> GetAllWardOfDistrict(string id)
        {
            return await Http.GetFromJsonAsync<List<Ward>>(baseurl + "/GetAllWardOfDistrict/" + id);
        }
        public async Task<Ward> GetWard(string id)
        {
            return await Http.GetFromJsonAsync<Ward>(baseurl + "/" + id);
        }
    }
}
