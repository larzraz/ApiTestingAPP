using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.Web.Http.Filters;

namespace ApiTesting.Data
{
    public class RequestManager
    {
#if __ANDRIOD__

        const string Url = "http://10.0.2.2:5000/api/requests/getall";
#else
        //const string Url = "http://127.0.0.1:5000/api/";
        const string Url = "http://10.0.2.2:5000/api/";

#endif


        private HttpClient GetClient()
        {
            

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        public async Task<Request> CreateNewRequest(Request request)
        {

            HttpClient client = GetClient();
            var response = await client.PostAsync(Url + "requests/create", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Request>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Request>> GetAll()
        {     
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url+ "requests/getall");
            return JsonConvert.DeserializeObject<IEnumerable<Request>>(result);
        }

        public async Task<Answer> AddAnswerToRequest(Answer answer)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url + "answers/create", new StringContent(JsonConvert.SerializeObject(answer), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Answer>(await response.Content.ReadAsStringAsync());

        }
    }
}
