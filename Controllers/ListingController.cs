/// Created By Frank Carusi
/// Controller for all listing related actions
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Controllers
{
    public class ListingController : Controller
    {
        private readonly IListingsProvider _listingProvider; // interface for all listing related operations that are consumed in the cotroller
        private readonly IHttpContextAccessor _httpContextAccessor; // interface for all User Session related operations that are consumed in the cotroller

        /// <summary>
        /// Inject the ListingsProvider and HttpContextAccessor into the controller
        /// </summary>
        /// <param name="listingsProvider"></param>
        /// <param name="httpContextAccessor"></param>
        public ListingController(IListingsProvider listingsProvider, IHttpContextAccessor httpContextAccessor)
        {
            _listingProvider = listingsProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Handle the default route for the Listing Controller and redirect to the AllListings action.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction("AllListings");
        }

        /// <summary>
        /// AllListings route, if the user is not logged in, redirect to the Login page.  
        /// Otherwise, get the current user from the session and return the AllListings view with the user's listings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AllListings()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("CurrentUser") == null) // check if the user is logged in
            {
                return RedirectToAction("Login", "User"); // if not, redirect to the login page
            }
            var session = _httpContextAccessor.HttpContext.Session; // if the user is logged in, get the current user from the session
            string value = session.GetString("CurrentUser"); 
            User user = JsonConvert.DeserializeObject<User>(value);
            return View(_listingProvider.GetUserListings(user.Id.ToString())); // return the AllListings view with the signed in user's listings
        }

        /// <summary>
        /// DeleteListing route, get the listing by id and return the DeleteListing view with the listing.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult DeleteListing(Guid id)
        {
            return View(_listingProvider.GetById(id));
        }

        /// <summary>
        /// DeleteListing POST route, delete the listing and redirect to the AllListings action.
        /// </summary>
        /// <param name="listing"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteListing(Listing listing)
        {
            _listingProvider.DeleteListing(listing);
            return RedirectToAction("AllListings");
        }

        /// <summary>
        /// ListingDetails route, get the listing by id and return the ListingDetails view with the listing and its images.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ListingDetails(Guid id)
        {
            Listing listing = _listingProvider.GetById(id);
            ViewBag.Images = _listingProvider.GetImagesByListingId(id); // get the listing's images and pass them to the view in the ViewBag

            return View(listing);
        }

        /// <summary>
        /// UpdateListing GET route, get the listing by id and return the UpdateListing view with the listing and its images.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateListing(Guid id)
        {
            Listing listing = _listingProvider.GetById(id);
            ViewBag.Images = _listingProvider.GetImagesByListingId(id); // get the listing's images and pass them to the view in the ViewBag

            return View(listing);
        }

        /// <summary>
        /// UpdateListing POST route, update the listing with the given input and redirect to the AllListings action.
        /// </summary>
        /// <param name="listing"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UpdateListing(IFormCollection listing)
        {

            _listingProvider.UpdateListing(listing); ;

            return RedirectToAction("AllListings");
        }

        /// <summary>
        /// CreateListing GET route, return the CreateListing view.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CreateListing()
        {

            return View();
        }

        /// <summary>
        /// CreateListing POST route, create the listing with the given input and redirect to the AllListings action.
        /// </summary>
        /// <param name="listing"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateListing(IFormCollection listing)
        {
            var session = _httpContextAccessor.HttpContext.Session; // get the current session
            string value = session.GetString("CurrentUser"); // get the current user from the session
            User user = JsonConvert.DeserializeObject<User>(value); // deserialize the user from the session into a User object
            string userId = user.Id.ToString(); // get the user's id as a string

            if (ModelState.IsValid) // check if the model is valid, if so, add the listing and redirect to the AllListings action
            {
                _listingProvider.AddListing(listing, userId);
                return RedirectToAction("AllListings");
            }
            return View(); // if the model is not valid, return the CreateListing view
        }
    }
}
