
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class ServiceService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Services";

        public async Task<List<ShareModel.Service>> GetAllService()
        {
            return await Http.GetFromJsonAsync<List<ShareModel.Service>>(baseurl);
        }
        public async Task<List<ShareModel.Service>> GetAllServiceOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<ShareModel.Service>>(baseurl + "/GetAllServiceOfOwner/" + phone);
        }
        public async Task<List<ShareModel.Service>> GetAllServiceOfHotel(int hotelId)
        {
            return await Http.GetFromJsonAsync<List<ShareModel.Service>>(baseurl + "/GetServiceOfHotel/" + hotelId);
        }
        public async Task<ShareModel.Service> GetService(int id)
        {
            return await Http.GetFromJsonAsync<ShareModel.Service>(baseurl + "/" + id);
        }
        public async Task<ShareModel.Service> UpdateService(Service_Custom service_Custom)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + service_Custom.ServiceId, service_Custom);
            if (res != null)
            {
                return await res.Content.ReadFromJsonAsync<ShareModel.Service>();
            }
            return null;
        }
        public async Task<ShareModel.Service> AddService(Service_Custom service_Custom)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", service_Custom);
            return await response.Content.ReadFromJsonAsync<ShareModel.Service>();
        }
    }
}
