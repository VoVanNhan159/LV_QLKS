using ShareModel;

namespace LV_QLKS.Service
{
    public class CustomerReviewService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Customerreviews";
        public async Task<List<Customerreview>> GetAllCustomerReviewOfHotel(int id)
        {
            return await Http.GetFromJsonAsync<List<Customerreview>>(baseurl + "/" + id);
        }
    }
}
