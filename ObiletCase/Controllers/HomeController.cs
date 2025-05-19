using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Models.Response;
using ObiletCase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ObiletCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly IObiletApiService _obiletApiService;
        public HomeController(IObiletApiService obiletApiService)
        {
            _obiletApiService = obiletApiService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetLocations(string term)
        {
            SessionResponse sessionResponse = await _obiletApiService.GetSessionWithCacheAsync();
            ResponseModel<List<LocationDataModel>> busResponse = await _obiletApiService.GetBusLocationsAsync(sessionResponse.Data, term);

            var result = busResponse.Data
                .Select(l => new { id = l.Id, text = l.Name })
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Journeys(JourneyDataModel journeyDataModel)
        {
            SessionResponse sessionResponse = await _obiletApiService.GetSessionWithCacheAsync();
            ResponseModel<List<JourneyDataModel>> journeyResponse = await _obiletApiService.GetJourneysAsync(sessionResponse.Data, journeyDataModel);
            ViewData["Journeys"] = journeyResponse.Data;
            return View();
        }

    }
}