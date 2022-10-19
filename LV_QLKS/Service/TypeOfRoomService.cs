
using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class TypeOfRoomService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/TypeOfRooms";
        public async Task<List<Typeofroom>> GetAllTypeOfRoom()
        {
            return await Http.GetFromJsonAsync<List<Typeofroom>>(baseurl);
        }
        public async Task<Typeofroom> GetTypeOfRoom(int id)
        {
            return await Http.GetFromJsonAsync<Typeofroom>(baseurl + "/" + id);
        }
        public async Task<List<Typeofroom>> GetAllTypeofroomOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Typeofroom>>(baseurl + "/GetAllTypeofroomOfOwner/" + phone);
        }
        //Thêm loại phòng
        public async Task<Typeofroom_Custom> AddTOR(Typeofroom_Custom typeOfRoom)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", typeOfRoom);
            return await response.Content.ReadFromJsonAsync<Typeofroom_Custom>();
        }
    }
}
