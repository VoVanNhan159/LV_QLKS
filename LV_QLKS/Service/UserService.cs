
using ShareModel;

namespace LV_QLKS.Service
{
    public class UserService
    {
        HttpClient Http = new HttpClient();
        string baseurl = "https://localhost:7282/api/Users";

        public async Task<User> GetUser(string id)
        {
            return await Http.GetFromJsonAsync<User>(baseurl + "/" + id);
        }
        public async Task<List<User>> GetAllUser()
        {
            return await Http.GetFromJsonAsync<List<User>>(baseurl);
        }
        public async Task<User> AddUser(User user)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", user);
            return await response.Content.ReadFromJsonAsync<User>();
        }
        public async Task<int> UpdateUser(User user)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + user.UserPhone, user);
            if (res.IsSuccessStatusCode)
            {
                return 1;
            }
            return 0;
        }
        private readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };
        //Trả về chuỗi không dấu
        public string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
    }
}
