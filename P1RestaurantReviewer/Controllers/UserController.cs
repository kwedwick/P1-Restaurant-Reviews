using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1RestaurantReviewer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P1RestaurantReviewer.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IReviewRepo _reviewRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private string userId;

        public UserController(IReviewRepo repo, UserManager<IdentityUser> usrMngr, ILogger<UserController> logger)
        {
            _reviewRepo = repo;
            _userManager = usrMngr;
            _logger = logger;
        }
        // GET: UserController
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                userId = _userManager.GetUserId(User);

                return View(_reviewRepo.GetMyReviews(userId));
            } catch (InvalidOperationException e)
            {
                _logger.LogError(e, $"Unable to find user reviews");
                return View();
            }
           
        }

        // GET: UserController/Details/5
        [Authorize]
        public ActionResult ReviewDetails(int id)
        {
            return View(_reviewRepo.GetReviewById(id));
        }

        // GET: UserController/Create
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: UserController/Edit/5
        [Authorize]
        [HttpGet]
        public ActionResult EditReview(int id)
        {

            try
            {
                Review foundReview = _reviewRepo.GetReviewById(id);
                if (foundReview != null)
                    return View(foundReview);
                else
                {
                    _logger.LogError($"Unable to find user's review: {id}");
                    return RedirectToAction("Index");
                }
                    
            } catch
            {
                _logger.LogCritical($"Encountered critical failure trying to get user review {id} to edit");
                return RedirectToAction("Index");
            }
            

        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditReview(int id, [Required] Review review)
        {
            if (!ModelState.IsValid)
            {
                return View("EditReview", id);
            }
            try
            {
                Review updatedReview = _reviewRepo.UpdateReview(id, review);
                _logger.LogInformation($"User has successfully updated their own review: {id}");
                return View("ReviewDetails", updatedReview);
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        [HttpGet]
        [Authorize]
        public ActionResult DeleteReview(int id)
        {
            Review foundReview = _reviewRepo.GetReviewById(id);
            if (foundReview != null)
                return View(foundReview);
            else
                return RedirectToAction("Index");
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReview(int id, Review review)
        {
            try
            {
                var deletedReview = _reviewRepo.DeleteReviewById(id);
                _logger.LogInformation($"User has deleted their review id: {id}");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _logger.LogError($"User was unable to delete their review: {id}");
                return View();
            }
        }
    }
}
