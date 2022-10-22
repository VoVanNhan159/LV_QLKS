using Microsoft.AspNetCore.WebUtilities;
using ShareModel;
using ShareModel.Custom;
using ShareModel.Paging;
using System.Text.Json;

namespace LV_QLKS.Service
{
    public class RoomService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Rooms";
        JsonSerializerOptions _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public async Task<Room> GetRoom(int id)
        {
            return await Http.GetFromJsonAsync<Room>(baseurl + "/" + id);
        }
        public async Task<List<Room>> GetAllRoomOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Room>>(baseurl + "/GetAllRoomOfOwner/" + phone);
        }
        public async Task<List<Room>> GetAllRoomOfHotel(int id)
        {
            var rooms = await Http.GetFromJsonAsync<List<Room>>(baseurl + "/GetAllRoomOfHotel/" + id);
            return rooms;
        }
        public double GetRateOfRoom(int id)
        {
            string starTemp = Http.GetStringAsync(baseurl + "/RateOfRoom/" + id).Result.ToString();
            double res = double.Parse(starTemp);
            return res;
        }
        //Thêm phòng
        public async Task<Room_Custom> AddRoom(Room_Custom room)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", room);
            return await response.Content.ReadFromJsonAsync<Room_Custom>();
        }
        public async Task<List<Room>> GetListRoomFilter(int hotelId, DateTime dayStart, DateTime dayEnd, int capacity)
        {
            return await Http.GetFromJsonAsync<List<Room>>(baseurl + "/GetListRoomFilter?hotelId=" + hotelId + "&dayStart=" + dayStart + "&dayEnd=" + dayEnd + "&capacity=" + capacity);
        }

        //Phân trang
        public async Task<PagingResponse<Room>> GetListPageHotel(PagingParameters paging)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = paging.PageNumber.ToString(),
                ["phone"] = paging.phone.ToString(),
                //["searchTerm"] = paging.SearchTerm == null ? "" : paging.SearchTerm
            };
            var response = await Http.GetAsync(QueryHelpers.AddQueryString(baseurl + "/PageNumberRooms", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<Room>
            {
                Items = JsonSerializer.Deserialize<List<Room>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };
            return pagingResponse;

        }
        public async Task<Room_Custom> UpdateRoom(Room_Custom room)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + room.RoomId, room);
            if (res != null)
            {
                return await res.Content.ReadFromJsonAsync<Room_Custom>();
            }
            return null;
        }
    }
}
