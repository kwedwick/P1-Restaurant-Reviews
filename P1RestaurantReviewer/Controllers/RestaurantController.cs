using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P1RestaurantReviewer.Domain;
using P1RestaurantReviewer.Models;

namespace P1RestaurantReviewer.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: RestaurantController
        private readonly IRestaurantRepo _repo;

        public RestaurantController(IRestaurantRepo repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View(_repo.GetAllRestaurants());

        }

        // GET: RestaurantController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View(nameof(Index));
            }
            var restaurant = _repo.GetAllRestaurants().First(x => x.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // GET: RestaurantController/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // POST: RestaurantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create(CreatedRestaurat viewModel)
        public ActionResult Create([Bind("Id,Name,Location,ZipCode")]CreatedRestaurant viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var restaurant = new Restaurant
            {
                Name = viewModel.Name,
                Location = viewModel.Location,
                ZipCode = viewModel.ZipCode
            };

            try
            {
               var restaurat = _repo.CreateRestaurant(restaurant);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                //ModelState.AddModelError(key: "Text", errorMessage: "Something went wrong. Please try again.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Details), new { id = restaurant.Id });
        }

        // GET: RestaurantController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public ActionResult WriteReview(int id)
        {
            return View();
        }

        // POST: Restaurant/CreateView/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WriteReview(int restaurantId, string userId)
        {
            if(!ModelState.IsValid)
            {
                return View(restaurantId);
            }

            //return RedirectToAction(nameof(Details), new { id = restaurant.Id });
            return View();
        }

        // GET: RestaurantController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
