using ObiletCase.Interface;
using ObiletCase.Models;
using ObiletCase.Models.Request;
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

        public async Task<ActionResult> Index()
        {
            SessionResponse sessionResponse = await _obiletApiService.GetSession();
            
            if (sessionResponse?.Data == null)
            {
                ViewBag.Error = "Session alınamadı!";
                return View("Error");
            }

            var busResponse = await _obiletApiService.GetBusLocationsAsync(sessionResponse.Data);
            var journeyResponse = await _obiletApiService.GetJourneysAsync(sessionResponse.Data, new JourneyDataModel { OriginId = 349, DestinationId = 356, DepartureDate = DateTime.Now });

            ViewBag.SessionId = sessionResponse.Data.SessionId;
            ViewData["Locations"] = busResponse.Data;
            ViewData["Journeys"] = journeyResponse.Data;


            return View();
        }
    }
}