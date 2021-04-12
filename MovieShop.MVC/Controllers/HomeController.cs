using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;

namespace MovieShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService, ILogger<HomeController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }
        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/


        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Logging non critical information {datetime}", DateTime.UtcNow);
            try
            {
                throw new Exception("my custom exception");
            }
            catch (Exception e)
            {
                _logger.LogError("caught exception {dateTime}", DateTime.UtcNow);
            }

            var movies = await _movieService.Get30HighestGrossing();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
