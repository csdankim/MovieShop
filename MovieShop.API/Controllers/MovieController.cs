using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Routing;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("toprevenue")]   // attribute based routing
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.Get30HighestGrossing();

            if (!movies.Any())
            {
                return NotFound("We did not find any movies");
            }

            return Ok(movies);

            // System.Text.Json in .NET Core 3
            // previous version of .NET 1, 2 and previous older .NET Framework Newtonsoft, 3rd party library
            // Serialization, convert your C# objects in the JSON objects
            // De-Serializatioin, convert JSON object to C# Object
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieAsync(id);
            if (movie==null)
            {
                return NotFound("We did not find any movies");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenre(genreId);
            if (!movies.Any())
            {
                return NotFound("We did not find any movies");
            }
            return Ok(movies);
        }
    }
}
