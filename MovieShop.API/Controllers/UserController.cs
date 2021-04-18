using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // localhost:4200/api/user/1/purchases
        [Authorize]
        [HttpGet]
        [Route("{id:int}/purchase")]
        public async Task<IActionResult> GetPurchaseMovies()
        {
            // get all movies purchased by user by calling service
            //var userPurchasedMovies = _userService.
            return Ok();
        }
    }
}
