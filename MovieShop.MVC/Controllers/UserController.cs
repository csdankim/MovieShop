using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserPurchaseMovies()
        {
            // HttpContext.User.Claims.Where(c => c.Type == "first");
            // call user Service by id of user to get all movies he/she purchased
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PurchaseMovie()
        {
            return View();
        }
    }
}
