using Microsoft.AspNetCore.Mvc;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.Models.Interfaces;
using PARKIT_enterprise_final.ViewModels;

namespace PARKIT_enterprise_final.Controllers
{
    /// <summary>
    /// Author: Syed Taha Faisal
    /// Description: This controller is responsible for managing user account view actions within the application.
    /// It handles the display and updating of user account information, including personal details and wallet data.
    /// It includes methods to render the account view, update user details, and update wallet information, 
    /// ensuring a seamless account management experience for the user.
    /// </summary>
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

        /// <summary>
        /// Handles requests to view the account page.
        /// This method retrieves the current user's ID and checks if the user is logged in. 
        /// If the user is not logged in, it redirects to the login page.
        /// Otherwise, it retrieves the user's details, bookings, and wallet information and passes them to the view.
        /// </summary>
        /// <returns>The account view with user, bookings, and wallet data, or redirects to the login page if the user is not logged in.</returns>
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

            var viewModel = new AccountsViewModel
            {
                User = user,
                Wallet = wallet,
                BookingList = bookings
            };

            return View(viewModel);
        }

        /// <summary>
        /// Handles the POST request to update user account information.
        /// This method removes certain model states (like 'User.Wallet' and 'User.Address') to focus on the updateable fields.
        /// If the model state is valid, it updates the user information and sets a success message.
        /// Otherwise, it sets an error message indicating the update failure.
        /// </summary>
        /// <returns>Redirects to the account page with either a success or error message based on the update operation result.</returns>
        [HttpPost]
        public IActionResult UpdateAccount([Bind(Prefix = "User")] User user)
        {
            ModelState.Remove("User.Wallet");
            ModelState.Remove("User.Address");

            if (ModelState.IsValid)
            {
                _userProvider.UpdateUser(user);
                TempData["AccountSuccessMessage"] = "Account Information updated successfully.";
                return RedirectToAction("Account", "Account");
            }
            TempData["AccountErrorMessage"] = "Error updating account. Please try again.";
            return RedirectToAction("Account", "Account");
        }

        /// <summary>
        /// Handles the POST request to update the user's wallet information.
        /// Validates the model state and updates the wallet details if valid.
        /// Sets a success message on successful update, or an error message if the update fails.
        /// </summary>
        /// <returns>Redirects to the account page with either a success or error message based on the wallet update operation result.</returns>
        [HttpPost]
        public IActionResult UpdateWallet([Bind(Prefix = "Wallet")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _walletProvider.UpdateWallet(wallet);
                TempData["WalletSuccessMessage"] = "Billing Information updated successfully.";
                return RedirectToAction("Account", "Account");
            }
            TempData["WalletErrorMessage"] = "Error updating billing information. Please try again.";
            return RedirectToAction("Account", "Account");
        }
    }
}