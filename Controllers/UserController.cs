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
            User userInDatabase = _userProvider.GetUserByUsername(user.Username);
            if(userInDatabase != null)
            {
                if (userInDatabase.Password == user.Password)
                {
                    // login successfully
                    // add login user to session
                    var session = _contextAccessor.HttpContext.Session;
                    session.SetString("CurrentUserName", userInDatabase.FirstName.ToString());
                    session.SetString("CurrentUser", JsonConvert.SerializeObject(userInDatabase));
                    return RedirectToAction("Account", "Home");
                }
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
            var session = _contextAccessor.HttpContext.Session;
            TempData["test"] = session.GetString("LoginUser");
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                User u = _userProvider.CreateUser(user);

                // need to change the destination
                return RedirectToAction("Map", "Home");
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


    }
}
