
using ShareModel;

namespace LV_QLKS.Service
{
    public class TypeOfAccountService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/TypeOfAccounts";

        public async Task<Typeofaccount> GetTypeOfAccount(int id)
        {
            return await Http.GetFromJsonAsync<Typeofaccount>(baseurl + "/" + id);
        }
        public async Task<List<Typeofaccount>> GetAllTypeOfAccount()
        {
            return await Http.GetFromJsonAsync<List<Typeofaccount>>(baseurl);
        }
    }
}
