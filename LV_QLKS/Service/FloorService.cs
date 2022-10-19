using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class FloorService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Floors";
        public async Task<List<Floor>> GetAllFloorOfHotel(int id)
        {
            return await Http.GetFromJsonAsync<List<Floor>>(baseurl + "/GetAllFloorOfHotel/" + id);
        }
        public async Task<List<Floor>> GetAllFloor()
        {
            return await Http.GetFromJsonAsync<List<Floor>>(baseurl);
        }
        //Lấy tất cả Floor của Owner
        public async Task<List<Floor>> GetAllFloorOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Floor>>(baseurl + "/GetAllFloorOfOwner/" + phone);
        }
        //Đếm phòng thuộc tầng
        public async Task<List<Room>> GetAllRoomOfFloor(int id)
        {
            var res = await Http.GetFromJsonAsync<List<Room>>(baseurl + "/CountRoomOfFloor/" + id);
            return res;
        }
        public async Task<Floor> GetFloor(int id)
        {
            return await Http.GetFromJsonAsync<Floor>(baseurl + "/" + id);
        }
        //Thêm tầng
        public async Task<Floor_Custom> AddFloor(Floor_Custom hotel)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", hotel);
            return await response.Content.ReadFromJsonAsync<Floor_Custom>();
        }
        //Cập nhật tầng
        public async Task<int> UpdateFloor(Floor floor)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + floor.FloorId, floor);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
    }
}
