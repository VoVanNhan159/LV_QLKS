using Microsoft.AspNetCore.WebUtilities;
using ShareModel;
using ShareModel.Custom;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LV_QLKS.Service
{
    public class HotelService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Hotels";
        JsonSerializerOptions _options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        //Lấy Hotel bằng ID
        public async Task<Hotel> GetHotel(int id)
        {
            return await Http.GetFromJsonAsync<Hotel>(baseurl + "/" + id);
        }
        //Lấy tất cả Hotel
        public async Task<List<Hotel>> GetAllHotel()
        {
            return await Http.GetFromJsonAsync<List<Hotel>>(baseurl);
        }
        //Lấy tất cả Hotel còn hạn đăng tin
        public async Task<List<Hotel>> GetAllHotelIsActive()
        {
            return await Http.GetFromJsonAsync<List<Hotel>>(baseurl+ "/GetAllHotelIsActive");
        }
        //Lấy tất cả Hotel còn hạn đăng tin của owner
        public async Task<List<Hotel>> GetAllHotelIsActiveOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Hotel>>(baseurl + "/GetAllHotelIsActiveOfOwner/" + phone);
        }
        //Lấy tất cả Hotel của Owner
        public async Task<List<Hotel>> GetAllHotelOfOwner(string owner_id)
        {
            return await Http.GetFromJsonAsync<List<Hotel>>(baseurl+ "/GetAllHotelOfOwner/"+owner_id);
        }
        //Lấy tất cả Hotel của Province
        public async Task<List<Hotel>> GetAllHotelOfProvince(string province_id)
        {
            return await Http.GetFromJsonAsync<List<Hotel>>(baseurl+ "/GetAllHotelOfProvince/" + province_id);
        }
        //Lấy địa chỉ của Hotel
        public string GetAddressHotel(int id)
        {
            return Http.GetStringAsync(baseurl + "/GetAddressHotel/" + id).Result;
        }
        //Lấy giá Fake của Hotel
        public int GetFakePriceHotel(int id)
        {
            return GetRealPriceHotel(id) + GetRealPriceHotel(id) / 100 * 30;
        }
        //Lấy giá Real của Hotel
        public int GetRealPriceHotel(int id)
        {
            return int.Parse(Http.GetStringAsync(baseurl + "/GetMinPriceHotel/" + id).Result);
        }
        //Format số thành kiểu VND
        public string FormatVND(int price)
        {
            return ((double)price).ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
        }
        public string GetUrlHotelDetail(int id)
        {
            return "/HotelDetail/" + id;
        }
        //Lấy tất cả Room của Hotel bằng ID
        public async Task<List<Room>> GetAllRoomOfHotel(int id)
        {
            return await Http.GetFromJsonAsync<List<Room>>(baseurl + "/GetAllRoomOfHotel/" + id);
        }
        //Lấy đánh giá của Hotel
        public double GetRateOfHotel(int id)
        {
            string starTemp = Http.GetStringAsync(baseurl + "/RateOfHotel/" + id).Result.ToString();
            double res = double.Parse(starTemp);
            return res;
        }
        //Đếm số lượng phòng của Hotel
        public int CountRoomOfHotel(int id)
        {
            string countTemp = Http.GetStringAsync(baseurl + "/CountRoomOfHotel/" + id).Result.ToString();
            int res = int.Parse(countTemp);
            return res;
        }

        public async Task<Hotel_Custom> AddHotel(Hotel_Custom hotel)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", hotel);
            return await response.Content.ReadFromJsonAsync<Hotel_Custom>();
        }
        public async Task<Hotel_Custom> UpdateHotel(Hotel_Custom hotel)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + hotel.HotelId, hotel);
            if (res != null)
            {
                return await res.Content.ReadFromJsonAsync<Hotel_Custom>();
            }
            return null;
        }
        public async Task<int> DeleteHotel(int id)
        {
            var res = await Http.DeleteAsync(baseurl + "/" + id);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
    }
}
