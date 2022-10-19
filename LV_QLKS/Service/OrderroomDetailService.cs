using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class OrderroomDetailService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Orderroomdetails";

        public async Task<List<Orderroomdetail>> GetAllOrderromDetailOfOrderrom(int orderrom_id)
        {
            return await Http.GetFromJsonAsync<List<Orderroomdetail>>(baseurl + "/" + orderrom_id);
        }
        public async Task<Orderroomdetail> AddOrderroomdetail(OrderroomDetail_Custom orderroomdetail)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", orderroomdetail);
            return await response.Content.ReadFromJsonAsync<Orderroomdetail>();
        }
        public async Task<int> DeleteOrderromDetailOfOrderrom(int odid, int room_id)
        {
            var res = await Http.DeleteAsync(baseurl + "/" + odid + "/" + room_id);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
    }
}
