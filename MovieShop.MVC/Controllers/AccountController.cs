using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            var user = await _userService.RegisterUser(model);
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model, string returnUrl=null)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl ??= Url.Content("~/");
            }

            var user = await _userService.ValidateUser(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("","Invalid Login Attempt");
                return View();
            }

            // continue for actual user
            // Cookie Based Authentication
            // Once your service tells you that u entered correct un/pw, application needs to create a authentication cookie that has some data and expiration time
            // FirstName, LastName, Dob
            // Encrypt information that you wanna store inside your cookie
            // Claims

            // Create Claims object with necessary information

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email), 
                new Claim("mywebsite","antra.com")
            };

            // Identity

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // creating the Cooke

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));


            return LocalRedirect(returnUrl);
        }
    }
}

/*
 * => movieshop.com/account/purchase => display all the movies purchased by user
 *
 * UserController
 *
 *     when user should be logged in 
 *     Purchases => should go to database and 
 *
 *AdminController
 * 
 *     ** user should be logged in
 *     ** user should have role of Admin
 *     [Authorize]
 *     CreateMovie
 *     [Authorize]
 *
 * 
 *
 */

