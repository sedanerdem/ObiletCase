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
    /// <summary>
    /// 
    /// </summary>
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
            ResponseModel<List<LocationDataModel>> busResponse = await _obiletApiService.GetBusLocationsAsync(term);

            var result = busResponse.Data
                .Select(l => new { id = l.Id, text = l.Name })
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}