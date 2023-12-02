using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.ViewModels;
using System.Diagnostics;

namespace PARKIT_enterprise_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapPointProvider _mapPointProvider;
        private readonly IListingsProvider _listingProvider;
        private readonly ILogger<HomeController> _logger;
        private readonly IBookingProvider _bookingProvider;
        private readonly IUserProvider _userProvider;
        private readonly IWalletProvider _walletProvider;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(
            ILogger<HomeController> logger, 
            IListingsProvider listingsProvider,
            IBookingProvider bookingProvider,
            IUserProvider userProvider,
            IWalletProvider walletProvider,
            IMapPointProvider mapPointProvider,
            IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _listingProvider = listingsProvider;
            _bookingProvider = bookingProvider;
            _userProvider = userProvider;
            _walletProvider = walletProvider;
            _mapPointProvider = mapPointProvider;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            if(_contextAccessor.HttpContext.Session.GetString("CurrentUser") != null)
            {
                return RedirectToAction("Account", "Home");
            }
            return RedirectToAction("CreateUser", "User");
        }

        public IActionResult Map()
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

        public IActionResult Account(Guid Id)
        {
            var viewModel = new AccountsViewModel
            {
/*                User = _userProvider.GetUser(Id),
                Wallet = _walletProvider.GetWallet*/
            };



            return View();
        }


    }
}