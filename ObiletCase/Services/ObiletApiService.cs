using ObiletCase.Constants;
using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObiletCase.Services
{
    public class ObiletApiService : IObiletApiService
    {
        private readonly ICallApiService _callApiService;
        private readonly ICacheService _cacheService;

        public ObiletApiService(ICallApiService callApiService, ICacheService cacheService)
        {
            _callApiService = callApiService;
            _cacheService = cacheService;
        }
        public async Task<SessionResponse> GetSession()
        {
            var bodyObject = new SessionRequestModel
            {
                Type = 1,
                Connection = new Connection
                {
                    IpAddress = ConnectionValues.IP_ADDRESS,
                    Port = ConnectionValues.PORT
                },
                Browser = new Browser
                {
                    Name =  ConnectionValues.BROWSER_NAME,
                    Version = ConnectionValues.BROWSER_VERSION
                }
            };
            
            return await _callApiService.CallApi<SessionRequestModel, SessionResponse>(UrlPaths.GET_SESSION, bodyObject);
        }

        public async Task<SessionResponse> GetSessionWithCacheAsync()
        {
            if (await _cacheService.ExistsAsync(RedisKeys.SESSION))
            {
                return await _cacheService.GetAsync<SessionResponse>(RedisKeys.SESSION);
            }

            var session = await GetSession();
            
            if (session.Status == Status.SUCCESS)
            {
                await _cacheService.SetAsync(RedisKeys.SESSION, session, TimeSpan.FromHours(1));
            }

            return session;
        }

        public async Task<ResponseModel<List<LocationDataModel>>> GetBusLocationsAsync(DeviceSession deviceSession, string searchText = null)
        {
            var bodyObject = new RequestModel<string>
            {
                Data = searchText,
                DeviceSession = deviceSession,
                Date = DateTime.Now,
                Language = Languages.TR
            };

            return await _callApiService.CallApi<RequestModel<string>, ResponseModel<List<LocationDataModel>>>(UrlPaths.GET_BUS_LOCATIONS, bodyObject);
        }


        public async Task<ResponseModel<List<LocationDataModel>>> GetBusLocationsCacheAsync()
        {
            if (await _cacheService.ExistsAsync(RedisKeys.BUS_LOCATIONS))
            {
                return await _cacheService.GetAsync<ResponseModel<List<LocationDataModel>>>(RedisKeys.BUS_LOCATIONS);
            }
           
            var session = await GetSessionWithCacheAsync();
            
            if(session == null || session.Data == null)
            {
                return new ResponseModel<List<LocationDataModel>>(); //error handling ekle.
            }

            var busLocations = await GetBusLocationsAsync(session.Data);
            if (busLocations.Status == Status.SUCCESS)
            {
                await _cacheService.SetAsync(RedisKeys.BUS_LOCATIONS, busLocations, TimeSpan.FromHours(1));
            }

            return busLocations;
        }


        public async Task<ResponseModel<List<JourneyDataModel>>> GetJourneysAsync(DeviceSession deviceSession, JourneyDataModel journeyDataModel)
        {
            var bodyObject = new RequestModel<JourneyDataModel>
            {
                Data = journeyDataModel,
                DeviceSession = deviceSession,
                Date = DateTime.Now,
                Language = Languages.TR
            };

            return await _callApiService.CallApi<RequestModel<JourneyDataModel>, ResponseModel<List<JourneyDataModel>>>(UrlPaths.GET_BUS_JOURNEYS, bodyObject);
        }
    }
}