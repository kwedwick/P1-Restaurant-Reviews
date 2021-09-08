using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P1RestaurantReviewer.Models;
using System.Text;
using P1RestaurantReviewer.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;

namespace P1RestaurantReviewer.Controllers
{
    /// <summary>
    /// This handles 
    /// </summary>
    [Authorize(Roles = "Administrator, Manager")]
    public class AdminController : Controller
    {
        private UserManager<IdentityUser> _userManager;

        private ILogger<AdminController> _logger;

        private IPasswordHasher<IdentityUser> _passwordHasher;

        public AdminController(UserManager<IdentityUser> usrMgr, IPasswordHasher<IdentityUser> passwordHash, ILogger<AdminController> logger)
        {
            _userManager = usrMgr;
            _passwordHasher = passwordHash;
            _logger = logger;
        }
        public string ReturnUrl { get; set; }

        public IActionResult Index(string searchString)
        {
            IEnumerable <IdentityUser> allUsers = _userManager.Users;


            if (!string.IsNullOrEmpty(searchString))
            {
                allUsers = allUsers.Where(s => s.UserName.Contains(searchString));
            }

            return View(allUsers);
        }

        public ViewResult Create() => View();

        /// <summary>
        /// Creating New User and sending Email Confirmation
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUserInput = new IdentityUser
                {
                    UserName = user.Username,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(newUserInput, user.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(newUserInput, "User").Wait();
                    return RedirectToAction("Index");
                }
                    
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        /// <summary>
        /// This finds the user by ID and returns back to Admin/Index if not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                IdentityUser user = await _userManager.FindByIdAsync(id);
                if (user != null)
                    return View(user);
                else
                    return RedirectToAction("Index");
            } catch (Exception e)
            {
                _logger.LogError(e, "Unable to add new user");
            }
            return View();
            
        }
        /// <summary>
        /// This updates the User if they are found with the id, email and password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string username, string password)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(email))
                    user.Email = email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(username))
                    user.UserName = username;
                else
                    ModelState.AddModelError("", "Username cannot be empty");

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = _passwordHasher.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");


                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
                _logger.LogDebug($"User was not found under admin/update with id: {id}");
                
            return View(user);
        }
        /// <summary>
        /// Deletes user from the database using Identity Method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Admin has deleted user");
                    return RedirectToAction("Index");
                }
                    
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            _logger.LogDebug($"Admin tried to delete user {id} but was not found");
            return View("Index", _userManager.Users);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}
