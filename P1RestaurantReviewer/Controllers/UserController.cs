using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private string userId;

        public UserController(IReviewRepo repo, UserManager<IdentityUser> usrMngr)
        {
            _reviewRepo = repo;
            _userManager = usrMngr;
        }
        // GET: UserController
        [Authorize]
        public ActionResult Index()
        {
            userId = _userManager.GetUserId(User);

            return View(_reviewRepo.GetMyReviews(userId));
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
            Review foundReview = _reviewRepo.GetReviewById(id);
            if (foundReview != null)
                return View(foundReview);
            else
                return RedirectToAction("Index");
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
