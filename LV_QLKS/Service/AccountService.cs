
using ShareModel;

namespace LV_QLKS.Service
{
    public class AccountService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Accounts";
        public async Task<Account> GetAccount(string id, string pwd)
        {
            return await Http.GetFromJsonAsync<Account>(baseurl + "/GetAccountLogin?id=" + id + "&pwd=" + pwd);
        }

        public async Task<Account> CheckAccount(string id)
        {
            return await Http.GetFromJsonAsync<Account>(baseurl + "/" + id);
        }
        public async Task<Account> AddAccount(Account acc)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", acc);
            return await response.Content.ReadFromJsonAsync<Account>();
        }
    }
}
