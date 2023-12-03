using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.ViewModels;

namespace PARKIT_enterprise_final.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBookingProvider _bookingProvider;
        private readonly IUserProvider _userProvider;
        private readonly IWalletProvider _walletProvider;

        public AccountController(IBookingProvider bookingProvider, IUserProvider userProvider, IWalletProvider walletProvider)
        {
            _bookingProvider = bookingProvider;
            _userProvider = userProvider;
            _walletProvider = walletProvider;
        }

        public IActionResult Account()
        {


            string userId = _bookingProvider.getUserId();

            if (userId == "false")
            {
                return RedirectToAction("Login", "User");
            }
            User user = _userProvider.GetUser(Guid.Parse(userId));

            Wallet wallet = _walletProvider.GetWallet(Guid.Parse(userId));

            List<Booking> bookings = _bookingProvider.GetAllBookings(Guid.Parse(userId));

/*            Booking List<Booking> = */


            var viewModel = new AccountsViewModel
            {
                User = user,
                Wallet = wallet,
                BookingList = bookings
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateAccount([Bind(Prefix = "User")] User user)
        {
            if (ModelState.IsValid)
            {
                _userProvider.UpdateUser(user);

                return RedirectToAction("Account", "Account");
            }

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult UpdateWallet([Bind(Prefix = "Wallet")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _walletProvider.UpdateWallet(wallet);

                return RedirectToAction("Account", "Account");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
