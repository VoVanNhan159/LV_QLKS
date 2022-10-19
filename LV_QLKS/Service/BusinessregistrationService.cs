using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class BusinessregistrationService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Businessregistrations";
        public async Task<Businessregistration> GetBusinessregistration(int id)
        {
            return await Http.GetFromJsonAsync<Businessregistration>(baseurl + "/" + id);
        }
        //Lấy tất cả Businessregistration
        public async Task<List<Businessregistration>> GetAllBusinessregistrationOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Businessregistration>>(baseurl+ "/GetAllBusinessregistrationOfOwner/"+phone);
        }
        //Add Businessregistration
        public async Task<Businessregistration_Custom> AddBusinessregistration(Businessregistration_Custom businessregistration)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", businessregistration);
            return await response.Content.ReadFromJsonAsync<Businessregistration_Custom>();
        }
    }
}
