using ObiletCase.Constants;
using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
using ObiletCase.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            ResponseModel<List<LocationDataModel>> busResponse = await _obiletApiService.GetBusLocationsCacheAsync(term);

            var result = busResponse.Data
                .Select(l => new { id = l.Id, text = l.Name })
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Journeys(JourneyDataModel journeyDataModel)
        {
            var destinationName = await _obiletApiService.GetJourneyNameById(journeyDataModel.DestinationId);
            var originName = await _obiletApiService.GetJourneyNameById(journeyDataModel.OriginId);

            ResponseModel<List<JourneyDataModel>> journeyResponse = await _obiletApiService.GetJourneysAsync(journeyDataModel);

            ViewData["Journeys"] = journeyResponse.Data;
            ViewData["OriginName"] = originName;
            ViewData["DestinationName"] = destinationName;
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}