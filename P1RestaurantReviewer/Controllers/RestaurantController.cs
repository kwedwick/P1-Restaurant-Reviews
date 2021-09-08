using Microsoft.AspNetCore.Http;
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
using Microsoft.Extensions.Logging;

namespace P1RestaurantReviewer.Controllers
{/// <summary>
/// Handles all restaurant routes and creating a review
/// </summary>
    public class RestaurantController : Controller
    {
        // GET: RestaurantController
        private readonly IRestaurantRepo _repo;
        private readonly IReviewRepo _reviewRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RestaurantController> _logger;
        public RestaurantController(IRestaurantRepo repo, IReviewRepo reviewRepo, UserManager<IdentityUser> userManager, ILogger<RestaurantController> logger)
        {
            _repo = repo;
            _reviewRepo = reviewRepo;
            _userManager = userManager;
            _logger = logger;
        }
        /// <summary>
        /// displays all restaurants in db and can sort by zip and search by name
        /// </summary>
        /// <param name="zipCode"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IActionResult Index(string zipCode, string searchString)
        {
            // Use LINQ to get list of genres.
            try
            {
                List<Restaurant> getAllRestaurants = _repo.GetAllRestaurants();

                _logger.LogInformation($"User has entered {searchString}");

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
            } catch (Exception e)
            {
                _logger.LogCritical(e, "Error with get request. Please try again.");
            }
            return View();
            
        }

        // GET: RestaurantController/Details/5
        /// <summary>
        /// displays details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                _logger.LogWarning("User was able to send through an id of null");
                return View(nameof(Index));
            }

            try
            {
                Restaurant restaurant = _repo.GetRestaurantById(id);
                restaurant.Reviews = _reviewRepo.GetReviewsbyRestaurantId(id);

                decimal sum = 0;
                int n = 0;
                if (restaurant.Reviews.Count != 0)
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
            catch (Exception e){ _logger.LogCritical(e, "User tried to get Restaurant Details but failed."); };

            return View();
        }

        // GET: RestaurantController/Create
        /// <summary>
        /// Create view to take in new data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        // POST: RestaurantController/Create
        /// <summary>
        /// Submits the users create restaurant
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind("Id,Name,Location,ZipCode")]CreatedRestaurant viewModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("User model was invalid");
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
                _logger.LogDebug("User has created a restaurant under restaurant/create");
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                //ModelState.AddModelError(key: "Text", errorMessage: "Something went wrong. Please try again.");
                _logger.LogCritical(e, "There was a critical error when user tried to create restaurant");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Details), new { id = restaurant.Id });
        }

        // GET: RestaurantController/Edit/5
        /// <summary>
        /// finds restaurant by ID to display in the edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Edit(int id)
        {
            Restaurant foundRestaurant = _repo.GetRestaurantById(id);
            foundRestaurant.Reviews = _reviewRepo.GetReviewsbyRestaurantId(id);
            if (foundRestaurant != null)
                return View(foundRestaurant);
            else
            {
                _logger.LogDebug($"Restaurant Edit failed to return restaurnt with id: {id}");
               return RedirectToAction("Index");
            }
                
        }

        // POST: RestaurantController/Edit/5
        /// <summary>
        /// Submits the users Edit data to the DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
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
                _logger.LogInformation($"User has updated restaurant details with id: {id}");
                return RedirectToAction(nameof(Details), new { id = updatedRestaurant.Id });
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Write Review UI
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult WriteReview()
        {
            return View();
        }

        // POST: Restaurant/CreateView/4
        /// <summary>
        /// Creates a review from restaurants/writereview/id - it takes in Users ID and Restaurant ID as well
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
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
                _logger.LogDebug("User submit a new review..");
                var newReview = _reviewRepo.CreateReview(review);
            }
            catch (InvalidOperationException e)
            {
                ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                _logger.LogCritical(e, $"There was an error creating a new review by user!");
                //ModelState.AddModelError(key: "Text", errorMessage: "Something went wrong. Please try again.");
                return View(viewModel);
            }
            return RedirectToAction(nameof(Details), new { id = review.RestaurantId });

        }

        // GET: RestaurantController/Delete/5
        /// <summary>
        /// Finds Restaurant by ID to display in the delete page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Delete(int id)
        {
            Restaurant foundRestaurant = _repo.GetRestaurantById(id);
            if (foundRestaurant != null)
                return View(foundRestaurant);
            else
            {
                _logger.LogCritical($"Admin/Manager tried to delete a user with id: {id} but they were not found!");
                return RedirectToAction("Index");
            }
                
        }

        // POST: RestaurantController/Delete/5
        /// <summary>
        /// Submits the Delete action to the DB and returns to the index view
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurant"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Delete(int id, Restaurant restaurant)
        {
            try
            {
                var deletedRestaurant = _repo.DeleteRestaurantById(id);
                _logger.LogDebug($"Manager/Admin has deleted user with id: {id}");
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException e)
            {
                _logger.LogCritical($"Admin/Manager encountered an error trying to delete user with id: {id}");
                return View();
            }
        }
    }
}
