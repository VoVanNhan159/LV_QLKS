using Microsoft.AspNetCore.Mvc;
using ShareModel;

namespace LV_QLKS.Service
{
    public class ImageService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Images";

        public async Task<Image> GetImage(int id)
        {
            return await Http.GetFromJsonAsync<Image>(baseurl + "/" + id);
        }
        public async Task<List<Image>> GetAllImage()
        {
            return await Http.GetFromJsonAsync<List<Image>>(baseurl);
        }
        public string GetURLImage(int id)
        {
            return Http.GetStringAsync(baseurl + "/GetURLImage/" + id).Result.ToString();
        }

        public async Task<string> GetAllImageOfHotel(int id)
        {
            return await Http.GetStringAsync(baseurl + "/GetAllImageOfHotel/" + id);
        }

        public async Task<Image> AddImage(Image image)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", image);
            return await response.Content.ReadFromJsonAsync<Image>();
        }
    }
}
