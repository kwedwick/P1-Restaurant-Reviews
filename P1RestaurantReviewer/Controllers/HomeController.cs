using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1RestaurantReviewer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using P1RestaurantReviewer.Domain;

namespace P1RestaurantReviewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRestaurantRepo _repo;
        public HomeController(ILogger<HomeController> logger, IRestaurantRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        
        /// <summary>
        /// Home page of website
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var random = new Random();
            var list = _repo.GetAllRestaurants();
            var randomList = list.OrderBy(x => random.NextDouble()).Take(3);
            return View(randomList);
        }
        /// <summary>
        /// Privacy policy page
        /// </summary>
        /// <returns></returns>
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
