using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class ImageHotelService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/ImageHotels";

        public async Task<List<ImageHotel>> GetAllImageHotel(int id)
        {
            return await Http.GetFromJsonAsync<List<ImageHotel>>(baseurl + "/GetImageOfHotel/" + id);
        }
        public async Task<ImageHotel_Custom> AddImageHotel(ImageHotel_Custom image)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", image);
            return await response.Content.ReadFromJsonAsync<ImageHotel_Custom>();
        }
    }
}
