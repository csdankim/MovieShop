using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApplicationCore.Models.Request;
using Infrastructure.Services;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private MovieService _movieService;

        public MoviesController()
        {
            _movieService = new MovieService();
        }
        public IActionResult Index()
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
        }

        // we want to show blank page with all the inputs
        [HttpGet]
        public IActionResult Create()
        {
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
