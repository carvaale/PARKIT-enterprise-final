/// <summary>
/// Created by Alexander Carvalho
/// Controller for the home page / map page
/// </summary>

using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.Models.Interfaces;
using System.Diagnostics;

namespace PARKIT_enterprise_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapPointProvider _mapPointProvider;


        public HomeController(IMapPointProvider mapPointProvider)
        {
            _mapPointProvider = mapPointProvider;
        }

        /// <summary>
        /// Map page to display all the parking spots
        /// Passes google api key to the view
        /// </summary>
        public IActionResult Index()
        {
            List<MapPoint> mapPoints = _mapPointProvider.GetMapPoints();
            ViewData["GMAP_API_KEY"] = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");

            return View(mapPoints);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}