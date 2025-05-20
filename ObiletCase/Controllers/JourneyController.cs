using ObiletCase.Interface;
using ObiletCase.Models.Request;
using ObiletCase.Models;
using ObiletCase.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace ObiletCase.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class JourneyController : Controller
    {
        private readonly IObiletApiService _obiletApiService;
        public JourneyController(IObiletApiService obiletApiService)
        {
            _obiletApiService = obiletApiService;
        }

        [HttpPost]
        public async Task<ActionResult> Index(JourneyDataModel journeyDataModel)
        {
            ResponseModel<List<JourneyDataModel>> journeyResponse = await _obiletApiService.GetJourneysAsync(journeyDataModel);

            ViewData["Journeys"] = journeyResponse.Data;
            ViewData["OriginName"] = journeyDataModel.OriginName;
            ViewData["DestinationName"] = journeyDataModel.DestinationName;
            return View();
        }


    }
}