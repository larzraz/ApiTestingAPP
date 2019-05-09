using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        const string Url = "http://127.0.0.1:7000/api/";
        //const string Url = "http://10.0.2.2:7000/api/";

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
            var response = await client.PostAsync(Url + "requests", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Request>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<Request>> GetAll()
        {     
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "requests");
            return JsonConvert.DeserializeObject<IEnumerable<Request>>(result);
        }

        public async Task<IEnumerable<Answer>> GetAnswersForRequestAsync(Request request)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(Url + "requests/"+ request.RequestId +"/answers");
            return JsonConvert.DeserializeObject<IEnumerable<Answer>>(result);
        }

       
        public async Task<Answer> AddAnswerToRequest(Answer answer, Request request)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(Url + "requests/" + request.RequestId + "/answer", new StringContent(JsonConvert.SerializeObject(answer), Encoding.UTF8, "application/json"));
            return JsonConvert.DeserializeObject<Answer>(await response.Content.ReadAsStringAsync());
        }
        public async void UpdateIsPreferredValueForAnswer(int answerID, Request request)
        {
            List<object> myObj = new List<object>
            {
                new { requestId = request.RequestId, answerId = answerID, isPreferred = true }
            };
            HttpClient client = GetClient();
            var method = new HttpMethod("PATCH");            
            HttpRequestMessage requestmessage = new HttpRequestMessage(method, Url + "answers/ispreferred")
            {
                
            };
            requestmessage.Content = new StringContent(JsonConvert.SerializeObject(myObj), Encoding.UTF8, "application/json-patch+json");
            var response = await client.SendAsync(requestmessage);
        }
        public async void CloseRequest( Request request)
        {
           
            request.IsClosed = request.IsClosed ? false : true;
            HttpClient client = GetClient();
            string json = JsonConvert.SerializeObject(request);
            await client.PatchAsync(Url + "requests/isClosed", new StringContent(json, Encoding.UTF8, "application/json"));
        }
        

               

            

        
    }
    
}
