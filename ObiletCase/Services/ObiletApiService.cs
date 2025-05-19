using ObiletCase.Constants;
using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObiletCase.Services
{
    public class ObiletApiService : IObiletApiService
    {
        #region Private Variables

        private readonly ICallApiService _callApiService;
        private readonly ICacheService _cacheService;
        private readonly ILogService _log;
        private readonly SessionResponse _session;
        
        #endregion Private Variables

        #region Constructor

        public ObiletApiService(ICallApiService callApiService, ICacheService cacheService, ILogService log)
        {
            _log = log;
            _callApiService = callApiService;
            _cacheService = cacheService;
            _session = GetSessionWithCacheAsync().Result;            
        }

        #endregion Constructor

        #region Public Methods

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
                    Name = ConnectionValues.BROWSER_NAME,
                    Version = ConnectionValues.BROWSER_VERSION
                }
            };

            return await _callApiService.CallApi<SessionRequestModel, SessionResponse>(UrlPaths.GET_SESSION, bodyObject);
        }

        public async Task<SessionResponse> GetSessionWithCacheAsync()
        {
            if (await _cacheService.ExistsAsync(RedisKeys.SESSION))
            {
                _log.Info("Session verileri cacheden getirildi - GetSessionWithCacheAsync");
                return await _cacheService.GetAsync<SessionResponse>(RedisKeys.SESSION);
            }

            var session = await GetSession();

            if (session.Status == Status.SUCCESS)
            {
                await _cacheService.SetAsync(RedisKeys.SESSION, session, TimeSpan.FromHours(1));
                _log.Info("Session verileri cachelendi - GetSessionWithCacheAsync");
            }
            else
            {
                _log.Error(new Exception(session.Message), "GetSessionWithCacheAsync");
                throw new Exception(session.Message);
            }

            return session;
        }

        public async Task<ResponseModel<List<LocationDataModel>>> GetBusLocationsAsync(string searchText = null)
        {
            var bodyObject = new RequestModel<string>
            {
                Data = searchText,
                DeviceSession = _session.Data,
                Date = DateTime.Now,
                Language = Languages.TR
            };

            return await _callApiService.CallApi<RequestModel<string>, ResponseModel<List<LocationDataModel>>>(UrlPaths.GET_BUS_LOCATIONS, bodyObject);
        }


        public async Task<ResponseModel<List<LocationDataModel>>> GetBusLocationsCacheAsync(string searchText = null)
        {
            if (await _cacheService.ExistsAsync(RedisKeys.BUS_LOCATIONS))
            {
                _log.Info("Lokasyon verileri cacheden getirildi - GetBusLocationsCacheAsync");
                return await _cacheService.GetAsync<ResponseModel<List<LocationDataModel>>>(RedisKeys.BUS_LOCATIONS);
            }

            var busLocations = await GetBusLocationsAsync(searchText);
            if (busLocations.Status == Status.SUCCESS)
            {
                await _cacheService.SetAsync(RedisKeys.BUS_LOCATIONS, busLocations, TimeSpan.FromHours(1));
                _log.Info("- Lokasyon verileri cachelendi - GetBusLocationsCacheAsync");
            }

            return busLocations;
        }


        public async Task<ResponseModel<List<JourneyDataModel>>> GetJourneysAsync(JourneyDataModel journeyDataModel)
        {
            var bodyObject = new RequestModel<JourneyDataModel>
            {
                Data = journeyDataModel,
                DeviceSession = _session.Data,
                Date = DateTime.Now,
                Language = Languages.TR
            };

            var response = await _callApiService.CallApi<RequestModel<JourneyDataModel>, ResponseModel<List<JourneyDataModel>>>(UrlPaths.GET_BUS_JOURNEYS, bodyObject);

            if (response.Status != Status.SUCCESS)
            {
                _log.Error(new Exception(response.UserMessage), " - GetJourneysAsync");
                throw new Exception(response.UserMessage);
            }

            _log.Info("Seyahat verileri çekildi - GetJourneysAsync");
            return response;
        }

        public async Task<string> GetJourneyNameById(long id)
        {
            ResponseModel<List<LocationDataModel>> busLocationResponse = await GetBusLocationsCacheAsync();
            _log.Info(" - GetJourneyNameById");
            return busLocationResponse.Data.FirstOrDefault(x => x.Id == id).Name;
        }

        #endregion Public Methods


    }
}