using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        //private MovieService _movieService;
        private readonly IMovieService _movieService;       // readonly -> to void tight coupling

        public MoviesController(IMovieService movieService)
        {
            //_movieService = new MovieService();
            _movieService = movieService;
        }
        /*public IActionResult Index()
        {
            // It will look for a View with name called Index (because the action method name is index)
            // return Index2, TestView
            // return View("TestView");

            // 1. ViewBag 2. ViewData 3. ** Strongly Typed Models
            // Send List of top 30 Movies to the View
            // id, title, posterUrl

            ViewBag.PageTitle = "Top 30 Grossing Movies";
            var movies = _movieService.Get30HighestGrossing();

            return View(movies);
        }*/

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.Get30HighestGrossing();
            return View(movies);
        }

        // we want to show blank page with all the inputs
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // should call Movie Service to get the details of the Movie that includes Movie Details, Cast for that Movie, Rating for that Movie
            return View();
        }

        // receive Movie information from View then submitted
        // interview question: model binding: map parameters
        [HttpPost]
        public IActionResult Create(MovieCreateRequestModel model) //, string title, string TITLE, string abc, decimal BUDGET, decimal bud)
        {
            _movieService.CreateMovie(model);
            return RedirectToAction("Index");
        }
    }
}
