using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AccountController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check data");
            }

            var registeredUser = await _userService.RegisterUser(model);
            return Ok(registeredUser);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser(LoginRequestModel model)
        {
            var loginUser = await _userService.ValidateUser(model.Email, model.Password);
            if (User == null)
            {
                return Unauthorized();
            }

            var jwtToken = _jwtService.GenerateToken(loginUser);
            // generate a JWT token and send it to Client
            return Ok(new {token = jwtToken});
        }

        /*[HttpGet]
        [Route("{id:int}", Name = "GetUser")]
        public async Task<ActionResult> GetUserByIdAsync(int id)
        {
            var user = await _userService.GetUserDetails(id);
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult> EmailExists([FromQuery] string email)
        {
            var user = await _userService.GetUser(email);
            return Ok(user == null ? new { emailExists = false } : new { emailExists = true });
        }*/
    }
}
