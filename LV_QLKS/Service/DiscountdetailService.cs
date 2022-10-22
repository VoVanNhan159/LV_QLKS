using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class DiscountdetailService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Discountdetails";
        public async Task<Discountdetail> GetDiscountdetail(int discountId, int roomId)
        {
            return await Http.GetFromJsonAsync<Discountdetail>(baseurl + "/" + discountId + "/" + roomId);
        }
        //Lấy tất cả Discountdetail của Discount
        public async Task<List<Discountdetail>> GetAllDiscountdetailOfDiscount(int id)
        {
            return await Http.GetFromJsonAsync<List<Discountdetail>>(baseurl + "/GetAllDiscountdetailOfDiscount/" + id);
        }
        //Lấy tất cả Discountdetail của Discount
        public async Task<List<Discountdetail>> GetAllDiscountdetailOfOwner(string phone)
        {
            return await Http.GetFromJsonAsync<List<Discountdetail>>(baseurl + "/GetAllDiscountdetailOfOwner/" + phone);
        }
        //Lấy tất cả Discountdetail còn hạn
        public async Task<List<Discountdetail>> GetAllDiscountdetailActive()
        {
            return await Http.GetFromJsonAsync<List<Discountdetail>>(baseurl + "/GetAllDiscountdetailActive/");
        }
        //Add Discountdetail
        public async Task<DiscountDetail_Custom> AddDiscountDetail(DiscountDetail_Custom discount)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", discount);
            return await response.Content.ReadFromJsonAsync<DiscountDetail_Custom>();
        }
        //Delete
        public async Task<int> DeleteDicountDetail(int discountId, int roomId)
        {
            var res = await Http.DeleteAsync(baseurl + "/" + discountId + "/" + roomId);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
        //Update
        public async Task<DiscountDetail_Custom> UpdateDiscountDetail(DiscountDetail_Custom DiscountDetail_Custom)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + DiscountDetail_Custom.DiscountId + "/" + DiscountDetail_Custom.RoomId, DiscountDetail_Custom);
            if (res != null)
            {
                var ress = await res.Content.ReadFromJsonAsync<DiscountDetail_Custom>();
                return ress;
            }
            return null;
        }
    }
}
