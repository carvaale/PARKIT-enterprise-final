using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PARKIT_enterprise_final.Models;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                string userName = user.Username;
                User userInDatabase = _userProvider.GetUserByUsername(userName);
                if (userInDatabase != null)
                {
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
            return View();
        }

        [HttpGet]
        public IActionResult AllUsers()
        {
            return View(_userProvider.GetUsers());
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                string userName = user.Username;
                User userInDatabase = _userProvider.GetUserByUsername(userName);
                if(userInDatabase == null)
                {
                    User u = _userProvider.CreateUser(user);
                    u.WalletId = u.Wallet.WalletId;
                    _userProvider.UpdateUser(u);

                    var session = _contextAccessor.HttpContext.Session;
                    session.SetString("CurrentUserName", u.FirstName.ToString());
                    session.SetString("CurrentUser", JsonConvert.SerializeObject(u));
                    // need to change the destination
                    return RedirectToAction("Account", "Account");
                }
                ViewBag.Error = "UserName already exists, please try another one!";
            }
            return View();
        }

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

        [HttpGet]
        public IActionResult UserDetails(Guid id)
        {
            User user = _userProvider.GetUser(id);

            return View(user);
        }

        [HttpGet]
        public IActionResult UpdateUser(Guid id)
        {
            User user = _userProvider.GetUser(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            _userProvider.UpdateUser(user); ;

            return RedirectToAction("AllUsers");
        }

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
