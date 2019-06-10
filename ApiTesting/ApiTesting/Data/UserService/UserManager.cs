using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiTesting.Data.UserService
{
   public class UserManager
    {
#if __ANDRIOD__

        const string Url = "http://10.0.2.2:5000/api/requests/getall";
#else
        //const string Url = "http://127.0.0.1:7000/api/";
        const string Url = "http://10.0.2.2:7000/api/";

#endif
        private HttpClient GetClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }
        public async Task<User> Register(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url + "users/register", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }

        public async Task<User> Login(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url + "users/login", new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }
    }
}
