using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class ImageRoomService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/ImageRooms";

        public async Task<List<ImageRoom>> GetAllImageRoom(int id)
        {
            return await Http.GetFromJsonAsync<List<ImageRoom>>(baseurl + "/" + id);
        }
        public async Task<ImageRoom_Custom> AddImageRoom(ImageRoom_Custom image)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", image);
            return await response.Content.ReadFromJsonAsync<ImageRoom_Custom>();
        }
    }
}
