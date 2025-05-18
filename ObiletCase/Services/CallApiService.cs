using Newtonsoft.Json;
using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ObiletCase.Services
{
    public class CallApiService : ICallApiService
    {
        private string _url;
        private string _token;

        public CallApiService()
        {
            _url = "https://v2-api.obilet.com/";
            _token = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";
        }

        public async Task<TResponse> CallApi<T, TResponse>(string path, T body)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_url);
                var request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Headers.Add("Authorization", $"Basic {_token}");
                var bodyObject = body;
                var jsonBody = JsonConvert.SerializeObject(bodyObject);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {response.StatusCode}\n{errorContent}");
                }

                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(jsonString);
            }
        }
    }
}