using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles="User, Manager, Administrator")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepo _repo;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(IReviewRepo repo, ILogger<ReviewController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        // GET: ReviewController
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            IEnumerable <Review> allReviews = _repo.GetAllReviews();


            if (!string.IsNullOrEmpty(searchString))
            {
                allReviews = allReviews.Where(s => s.RestaurantName.Contains(searchString));
            }

            return View(allReviews);
        }

        // GET: ReviewController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                Review singleReview = _repo.GetReviewById(id);
                return View(singleReview);

            } catch (InvalidOperationException e)
            {
                _logger.LogCritical(e, $"Unable to get Details of review id: {id}");
                return RedirectToAction("Index");
            }
        }

        // GET: ReviewController/Edit/5
        [Authorize(Roles ="Administrator, Manager")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                Review foundReview = _repo.GetReviewById(id);
                if (foundReview != null)
                    return View(foundReview);
                else
                    return RedirectToAction("Index");
            } catch (InvalidOperationException e)
            {
                _logger.LogCritical(e, $"Unable to see (not submit) EDIT REVIEW of review id: {id}");
                return RedirectToAction("Index");
            }
            
        }


        // POST: ReviewController/Edit/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Required] Review review)
        {
            if (!ModelState.IsValid)
            {
                return View("EditReview", id);
            }
            try
            {
                Review updatedReview = _repo.UpdateReview(id, review);
                return View("Details", updatedReview);
            }
            catch (InvalidOperationException e)
            {
                _logger.LogCritical(e, $"Unable to submit EDIT REVIEW of review id: {id}");
                return RedirectToAction("Index");
            }
        }

        // GET: ReviewController/Delete/5
        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public ActionResult Delete(int id)
        {
            Review foundReview = _repo.GetReviewById(id);
            if (foundReview != null)
                return View(foundReview);
            else
                return RedirectToAction("Index");
        }

        // POST: ReviewController/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Review review)
        {
            try
            {
                var deletedReview = _repo.DeleteReviewById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Unable to delete review/delete id: {id}");
                return View();
            }
        }
    }
}
