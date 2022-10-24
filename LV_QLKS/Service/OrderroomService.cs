using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class OrderroomService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Orderrooms";

        public async Task<Orderroom> GetOrderrom(int id)
        {
            return await Http.GetFromJsonAsync<Orderroom>(baseurl + "/" + id);
        }
        public async Task<List<Orderroom>> GetAllOrderrom()
        {
            return await Http.GetFromJsonAsync<List<Orderroom>>(baseurl);
        }
        public async Task<List<Orderroom>> GetAllOrderromOfUser(string phone)
        {
            return await Http.GetFromJsonAsync<List<Orderroom>>(baseurl + "/GetAllOrderromOfUser/" + phone);
        }
        public async Task<List<Orderroom>> GetAllOrderroomOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Orderroom>>(baseurl + "/GetAllOrderroomOfOwner/" + phone);
        }
        public async Task<Orderroom> AddOrderroomCustom(Orderroom_Custom orderroom)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/AddOrderroomCustom/", orderroom);
            return await response.Content.ReadFromJsonAsync<Orderroom>();
        }
        public async Task<Orderroom> AddOrderroom(Orderroom orderroom)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", orderroom);
            return await response.Content.ReadFromJsonAsync<Orderroom>();
        }
        public async Task<int> DeleteOrderrom(int odid)
        {
            var res = await Http.DeleteAsync(baseurl + "/" + odid);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
        public async Task<Orderroom> UpdateOrderroomAfterPayment(Orderroom_Custom orderroom)
        {
            var res = await Http.PutAsJsonAsync(baseurl+"/"+orderroom.OrderroomId, orderroom);
            return await res.Content.ReadFromJsonAsync<Orderroom>();
        }
    }
}
