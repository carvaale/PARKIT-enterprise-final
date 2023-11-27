﻿using Microsoft.AspNetCore.Mvc;
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
                    return View(user);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AllUsers()
        {
            return View(_userProvider.GetUsers());
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                User u = _userProvider.CreateUser(user);

                // need to change the destination
                return RedirectToAction("AllUsers");
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
