using ApplicationCore.Contracts.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    // Controllers => Services (BL) => Repositories => Database using EF Core or Dapper or both
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        // Action methods
        public IActionResult Index()
        {

            // go to database and get the data
            // tightly coupled code
            // losely coupled code
            // 

            var movies = _movieService.GetTop30GrossingMovies();

            // 3 ways we can send the data from controller/action method to views
            // ViewBag
            // ViewData
            // *** Strongly Typed  Models **

            return View(movies);
        }

        // http://movieshop.com/home/privacy
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