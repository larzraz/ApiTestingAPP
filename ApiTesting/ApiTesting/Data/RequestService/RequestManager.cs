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
        const string Url = "http://127.0.0.1:7001/api/";
        //const string Url = "http://10.0.2.2:7001/api/";

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

            HttpClient client = GetClient();
            var method = new HttpMethod("PATCH");

            string jsonData = "{\"requestId\":" + request.RequestId + ", \"answerId\":" + answerID + ", \"isPreferred\": true}";

            string jData = @"{
            'requestId':'" + request.RequestId + ", 'answerId':" + answerID + ", 'isPreferred': 'true'}";
           

            var requestmessage = new HttpRequestMessage(method, Url + "requests/" + request.RequestId + "/answer/" + answerID + "/correct")
            {
                Content = new StringContent(jData, Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(requestmessage);
        }
        public async void CloseRequest( Request request)
        {

            HttpClient client = GetClient();
            var method = new HttpMethod("PATCH");
            var requestmessage = new HttpRequestMessage(method, Url + "requests/" + request.RequestId + "/close")
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(requestmessage);
        }


    }
    
}
