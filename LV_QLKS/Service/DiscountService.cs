using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class DiscountService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Discounts";
        public async Task<Discount> GetDiscount(int id)
        {
            return await Http.GetFromJsonAsync<Discount>(baseurl + "/" + id);
        }
        //Lấy tất cả Discount của Owner
        public async Task<List<Discount>> GetAllDiscountOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Discount>>(baseurl + "/GetAllDiscountOfOwner/" + phone);
        }
        //Add Businessregistration
        public async Task<Discount_Custom> AddDiscount(Discount_Custom discount)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", discount);
            return await response.Content.ReadFromJsonAsync<Discount_Custom>();
        }
        
        public async Task<int> DeleteDicount(int id)
        {
            var res = await Http.DeleteAsync(baseurl + "/" + id);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
        public async Task<int> UpdateDiscount(Discount discount)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + discount.DiscountId, discount);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
    }
}
