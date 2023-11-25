using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.ViewModels;

namespace PARKIT_enterprise_final.Controllers
{
    public class BookingController : Controller
    {
        private readonly IListingsProvider _listingProvider;
        private readonly IUserProvider _userProvider;

        public BookingController(IListingsProvider listingsProvider)
        {
            _listingProvider = listingsProvider;
        }


        [HttpGet]
        public IActionResult BookListing(Guid Id)
        {
            //Retreives single listing through Id
            Listing listing = _listingProvider.GetById(Id);


            var viewModel = new ListingDetailsViewModel
            {
                Listing = listing,
            };

            return View(viewModel);

        }
    }
    }
