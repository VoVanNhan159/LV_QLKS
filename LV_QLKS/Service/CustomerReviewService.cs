using ShareModel;
using ShareModel.Custom;

namespace LV_QLKS.Service
{
    public class CustomerReviewService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Customerreviews";
        public async Task<Customerreview> GetCustomerreview(int id)
        {
            return await Http.GetFromJsonAsync<Customerreview>(baseurl + "/" + id);
        }
        public async Task<List<Customerreview>> GetAllCustomerReviewOfHotel(int id)
        {
            return await Http.GetFromJsonAsync<List<Customerreview>>(baseurl + "/GetAllCustomerreviewOfHotel/" + id);
        }
        public async Task<List<Customerreview>> GetAllCustomerreviewOfRoom(int id)
        {
            return await Http.GetFromJsonAsync<List<Customerreview>>(baseurl + "/GetAllCustomerreviewOfRoom/" + id);
        }
        public async Task<CustomerReview_Custom> AddCustomerreviewOfRoom(CustomerReview_Custom customerReview_Custom)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", customerReview_Custom);
            return await response.Content.ReadFromJsonAsync<CustomerReview_Custom>();
        }
    }
}
