﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P1RestaurantReviewer.Domain;
using P1RestaurantReviewer.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace P1RestaurantReviewer.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: RestaurantController
        private readonly IRestaurantRepo _repo;
        private readonly IReviewRepo _reviewRepo;
        private readonly UserManager<IdentityUser> _userManager;
        public RestaurantController(IRestaurantRepo repo, IReviewRepo reviewRepo, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _reviewRepo = reviewRepo;
            _userManager = userManager;
        }

        public IActionResult Index(string zipCode, string searchString)
        {
            // Use LINQ to get list of genres.
            List<Restaurant> getAllRestaurants = _repo.GetAllRestaurants();

            var nameQuery = from r in getAllRestaurants
                                           orderby r.ZipCode
                                             select r.ZipCode;

             var restaurants = from r in _repo.GetAllRestaurants()
                          select r;

             if (!string.IsNullOrEmpty(searchString))
             {
                 restaurants = restaurants.Where(s => s.Name.Contains(searchString));
             }

             if (!string.IsNullOrEmpty(zipCode))
             {
                 restaurants = restaurants.Where(x => x.ZipCode == Convert.ToInt32(zipCode));
             }

             var restaurantNameVM = new RestaurantNameViewModel
             {
                 ZipCode = new SelectList(nameQuery.Distinct().ToList()),
                 Restaurants = restaurants.ToList()
             };

             return View(restaurantNameVM);
        }

        // GET: RestaurantController/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return View(nameof(Index));
            }
            Restaurant restaurant = _repo.GetRestaurantById(id);
            restaurant.Reviews = _reviewRepo.GetReviewsbyRestaurantId(id);

            decimal sum = 0;
            int n = 0;
            if(restaurant.Reviews.Count != 0)
            {
                for (int i = 0; i < restaurant.Reviews.Count; i++)
                {
                    sum += Convert.ToDecimal(restaurant.Reviews[i].Rating);
                    n += 1;
                }
                decimal average = (sum / n);
                            decimal average1 = Math.Round(average, 2);
                            restaurant.AverageRating = average1;
            }
            else
            {
                restaurant.AverageRating = 0;
            }
            
            

            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // GET: RestaurantController/Create
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        // POST: RestaurantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Edit(int id)
        {
            Restaurant foundRestaurant = _repo.GetRestaurantById(id);
            if (foundRestaurant != null)
                return View(foundRestaurant);
            else
                return RedirectToAction("Index");
        }

        // POST: RestaurantController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Edit(int id, [Required] Restaurant restaurant)
        {
            if(!ModelState.IsValid)
            {
                return View("Edit", id);
            }
            try
            {
                Restaurant updatedRestaurant = _repo.UpdateRestaurant(id, restaurant);
                return View("Details", updatedRestaurant);
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult WriteReview()
        {
            return View();
        }

        // POST: Restaurant/CreateView/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult WriteReview(int id, CreateReview viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var review = new Review
            {
                Title = viewModel.Title,
                Body = viewModel.Body,
                Rating = Convert.ToInt32(viewModel.Rating),
                UserId = _userManager.GetUserId(User),
                RestaurantId = id
            };

            try
            {
                var newReview = _reviewRepo.CreateReview(review);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                //ModelState.AddModelError(key: "Text", errorMessage: "Something went wrong. Please try again.");
                return View(viewModel);
            }
            return RedirectToAction(nameof(Details), new { id = review.RestaurantId });

        }

        // GET: RestaurantController/Delete/5
        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Delete(int id)
        {
            Restaurant foundRestaurant = _repo.GetRestaurantById(id);
            if (foundRestaurant != null)
                return View(foundRestaurant);
            else
                return RedirectToAction("Index");
        }

        // POST: RestaurantController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Delete(int id, Restaurant restaurant)
        {
            try
            {
                var deletedRestaurant = _repo.DeleteRestaurantById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
