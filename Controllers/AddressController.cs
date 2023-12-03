/// <summary>
/// Created by Ruiyan Shi 
/// standard controller for the Address object, used for control the traffic of Address
/// </summary>
using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Controllers
{
    public class AddressController : Controller
    {
        // interface to access address provider
        private readonly IAddressProvider _addressProvider;
        public AddressController(IAddressProvider addressProvider)
        {
            _addressProvider = addressProvider;
        }

        // default index controller
        public IActionResult Index()
        {
            return View();
        }

        // get and post function to edit address
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Address address = _addressProvider.GetAddress(id);
            return View(address);
        }
        [HttpPost]
        public IActionResult Edit(Address address)
        {
            _addressProvider.UpdateAddress(address); ;

            // need to change to a new destination
            return RedirectToAction("AllAddresses");
        }

        // get all addresses and return the list to view
        public IActionResult AllAddresses()
        {
            return View(_addressProvider.GetAddresses());
        }
    }
}

