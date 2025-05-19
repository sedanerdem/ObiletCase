using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ObiletCase.Interface
{
    public interface IObiletApiService
    {
        Task<SessionResponse> GetSession();
        Task<SessionResponse> GetSessionWithCacheAsync();
        Task<ResponseModel<List<LocationDataModel>>> GetBusLocationsAsync(DeviceSession deviceSession, string searchText = null);
        Task<ResponseModel<List<JourneyDataModel>>> GetJourneysAsync(DeviceSession deviceSession, JourneyDataModel journeyDataModel);
    }
}