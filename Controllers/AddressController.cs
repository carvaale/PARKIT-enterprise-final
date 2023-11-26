using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressProvider _addressProvider;
        public AddressController(IAddressProvider addressProvider)
        {
            _addressProvider = addressProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

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

        public IActionResult AllAddresses()
        {
            return View(_addressProvider.GetAddresses());
        }
    }
}

