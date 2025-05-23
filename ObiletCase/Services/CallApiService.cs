﻿using Newtonsoft.Json;
using ObiletCase.Constants;
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

        /// <summary>
        /// 
        /// </summary>
        public CallApiService()
        {
            _url = ConnectionValues.URL;
            _token = ConnectionValues.TOKEN;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="path"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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