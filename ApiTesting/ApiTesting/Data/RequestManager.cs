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
        const string Url = "http://127.0.0.1:5000/api/requests/getall";
        //const string Url = "http://10.0.2.2:5000/api/requests/getall";

#endif


        private HttpClient GetClient()
        {
            

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        public async Task<IEnumerable<Request>> GetAll()
        {     
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<Request>>(result);
        }
    }
}
