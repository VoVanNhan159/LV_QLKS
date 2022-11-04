using System.Security.Cryptography;
using ShareModel;
using System.Text;
using ShareModel.Custom;

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
        public async Task<Account> UpdateAccount(Account_Custom account_Custom)
        {
            var res = await Http.PutAsJsonAsync(baseurl + "/" + account_Custom.AccountUsername, account_Custom);
            if (res != null)
            {
                return await res.Content.ReadFromJsonAsync<Account>();
            }
            return null;
        }
        public async Task<Account> CheckAccount(string id)
        {
            try
            {
                return await Http.GetFromJsonAsync<Account>(baseurl + "/" + id);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public async Task<Account> AddAccount(Account acc)
        {
            var response = await Http.PostAsJsonAsync(baseurl + "/", acc);
            return await response.Content.ReadFromJsonAsync<Account>();
        }
        private readonly string key = "Luanvantotngiep";
        public string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        var temp = Convert.ToBase64String(bytes, 0, bytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }
        public string Decrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] textBytes = Convert.FromBase64String(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        var temp = Convert.ToBase64String(bytes, 0, bytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}
