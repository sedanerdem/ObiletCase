using Newtonsoft.Json;
using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Models.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ObiletCase.Services
{
    public class ObiletApiService : IObiletApiService
    {
        private readonly ICallApiService _callApiService;
        public ObiletApiService(ICallApiService callApiService)
        {
            _callApiService = callApiService;
        }
        public async Task<SessionResponse> GetSession()
        {
            var bodyObject = new SessionRequestModel
            {
                Type = 1,
                Connection = new Connection
                {
                    IpAddress = "1.1.1.1",
                    Port = "5117"
                },
                Browser = new Browser
                {
                    Name = "Chrome",
                    Version = "47.0.0.12"
                }
            };

            return await _callApiService.CallApi<SessionRequestModel, SessionResponse>("api/client/getsession", bodyObject);
        }

        public async Task<ResponseModel<List<LocationDataModel>>> GetBusLocationsAsync(DeviceSession deviceSession, string searchText = null)
        {
            var bodyObject = new RequestModel<string>
            {
                Data = searchText,
                DeviceSession = deviceSession,
                Date = DateTime.Now,
                Language = "tr-TR"
            };

            return await _callApiService.CallApi<RequestModel<string>, ResponseModel<List<LocationDataModel>>>("api/location/getbuslocations", bodyObject);
        }

        public async Task<ResponseModel<List<JourneyDataModel>>> GetJourneysAsync(DeviceSession deviceSession, JourneyDataModel journeyDataModel)
        {
            var bodyObject = new RequestModel<JourneyDataModel>
            {
                Data = journeyDataModel,
                DeviceSession = deviceSession,
                Date = DateTime.Now,
                Language = "tr-TR"
            };
            
            return await _callApiService.CallApi<RequestModel<JourneyDataModel>, ResponseModel<List<JourneyDataModel>>>("api/journey/getbusjourneys", bodyObject);
        }
    }
}