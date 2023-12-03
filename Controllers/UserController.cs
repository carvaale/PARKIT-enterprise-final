/// <summary>
/// Created by Ruiyan Shi 
/// controller for the User object, used for control the traffic of User
/// </summary>
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PARKIT_enterprise_final.Models;
using PARKIT_enterprise_final.ViewModels;
using PARKIT_enterprise_final.Models.Interfaces;

namespace PARKIT_enterprise_final.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserProvider _userProvider;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserProvider userProvider, IHttpContextAccessor httpContextAccessor)
        {
            _userProvider = userProvider;
            _contextAccessor = httpContextAccessor;
        }

        // default user index
        public IActionResult Index()
        {
            return View();
        }

        // controller for login
        // get login returns the login view
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // post login will accept users input of username and password
        // then check the password with database
        // if the password match, will log the uer in
        // if not, return the error message
        [HttpPost]
        public IActionResult Login(UserLoginViewModel user)
            // using userloginverModel to accept only username and password
        {
            if (ModelState.IsValid)
            {
                string userName = user.UserName;
                // get the user from data base by username
                User userInDatabase = _userProvider.GetUserByUsername(userName);
                //  if the database returns an existing user or null
                if (userInDatabase != null)
                {
                    // if the password matches
                    if (userInDatabase.Password == user.Password)
                    {
                        // login successfully
                        // add login user to session
                        var session = _contextAccessor.HttpContext.Session;
                        session.SetString("CurrentUserName", userInDatabase.FirstName.ToString());
                        session.SetString("CurrentUser", JsonConvert.SerializeObject(userInDatabase));
                        return RedirectToAction("Account", "Account");
                    }
                }
                ViewBag.Error = "UserName and Password doesn't match, please try again";
                return View();
            }
            // add the error message and pass back to view by viewbag
            
            ViewBag.Error = "Some Error Occurs, please report to the developer";
            return View();
        }

        // get all users
        [HttpGet]
        public IActionResult AllUsers()
        {
            return View(_userProvider.GetUsers());
        }

        // get function to get the page for creating new user
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        // post request to create new user
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            // if the user input is valid
            if (ModelState.IsValid)
            {
                // check if the username is used already
                string userName = user.Username;
                User userInDatabase = _userProvider.GetUserByUsername(userName);
                // if returns null mean not used
                if(userInDatabase == null)
                {
                    // create the user
                    User u = _userProvider.CreateUser(user);
                    // set the wallet ID and update it
                    u.WalletId = u.Wallet.WalletId;
                    _userProvider.UpdateUser(u);

                    // add the login info to the session after register successfully
                    var session = _contextAccessor.HttpContext.Session;
                    session.SetString("CurrentUserName", u.FirstName.ToString());
                    session.SetString("CurrentUser", JsonConvert.SerializeObject(u));
                    // need to change the destination
                    return RedirectToAction("Account", "Account");
                }
                // return to the view with the error message
                ViewBag.Error = "UserName already exists, please try another one!";
            }
            return View();
        }

        // delete the user
        [HttpGet]
        public IActionResult DeleteUser(Guid id)
        {
            return View(_userProvider.GetUser(id));
        }

        [HttpPost]
        public IActionResult DeleteUser(User user)
        {
            _userProvider.DeleteUser(user);
            return RedirectToAction("AllUsers");
        }

        // check user details
        [HttpGet]
        public IActionResult UserDetails(Guid id)
        {
            User user = _userProvider.GetUser(id);

            return View(user);
        }

        // update user, using id to get existing user info
        [HttpGet]
        public IActionResult UpdateUser(Guid id)
        {
            User user = _userProvider.GetUser(id);
            return View(user);
        }

        // post request to update the user in the database
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            _userProvider.UpdateUser(user); ;

            return RedirectToAction("AllUsers");
        }

        // logout the user by clear the session
        [HttpGet]
        public IActionResult LogoutUser()
        {
            var session = _contextAccessor.HttpContext.Session;
            session.SetString("CurrentUserName", "");
            session.SetString("CurrentUser", "");
            _contextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Account", "Account");
        }


    }
}
