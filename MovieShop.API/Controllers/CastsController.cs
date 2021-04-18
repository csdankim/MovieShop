using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CastsController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastsController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCastWithMovies(int id)
        {
            var cast = await _castService.GetCastDetailsWithMovies(id);
            if (cast == null)
            {
                return NotFound("We did not find any cast");
            }
            return Ok(cast);
        }
    }
}
