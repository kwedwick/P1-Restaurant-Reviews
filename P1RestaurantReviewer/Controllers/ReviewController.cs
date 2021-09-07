using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ReviewController(IReviewRepo repo)
        {
            _repo = repo;
        }
        // GET: ReviewController
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
        public ActionResult Details(int id)
        {
            return View(_repo.GetReviewById(id));
        }

        // GET: ReviewController/Edit/5
        [Authorize(Roles ="Administrator, Manager")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Review foundReview = _repo.GetReviewById(id);
            if (foundReview != null)
                return View(foundReview);
            else
                return RedirectToAction("Index");
        }


        // POST: ReviewController/Edit/5
        [HttpPost]
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
            catch
            {
                return View();
            }
        }

        // GET: ReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReviewController/Delete/5
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
