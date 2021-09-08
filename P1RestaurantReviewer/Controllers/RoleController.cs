using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P1RestaurantReviewer.Models;

namespace P1RestaurantReviewer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        //ready role manager class
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RoleController> _logger;

        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userMgr, ILogger<RoleController> logger)
        {
            _roleManager = roleMgr;
            _userManager = userMgr;
            _logger = logger;
        }
        [HttpGet]
        public ViewResult Index() => View(_roleManager.Roles);

       [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                    if (result.Succeeded)
                    {
                        _logger.LogDebug($"User has created new role: {name}");
                        return RedirectToAction("Index");
                    }
                        
                    else
                        Errors(result);
                }
                catch (InvalidOperationException e)
                {
                    _logger.LogError($"There was an error creating a new role: {name}. Potentially already exists.");
                    ModelState.AddModelError(key: "Text", errorMessage: e.Message);
                    ModelState.AddModelError(key: "Text", errorMessage: "Something went wrong. Please use a different role name as it may already exist.");
                    return View("Create", name);
                }
            }
            return View("Create", name);
        }

        [HttpPost]

        //public IActionResult Delete() => View();
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                _logger.LogInformation($"User selected user: {id} to delete");
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User has successfully deleted user: {id}.");
                    return RedirectToAction("Index");
                }
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", _roleManager.Roles);
        }

        /// <summary>
        /// This method is used to get memebers and non-members of a selected Identity Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<IdentityUser> members = new List<IdentityUser>();
            List<IdentityUser> nonMembers = new List<IdentityUser>();
            List<IdentityUser> users = _userManager.Users.ToList();

            foreach (IdentityUser user in users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        /// <summary>
        /// This method is used for adding or removing users from an Identity Role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        _logger.LogInformation($"User has successfully added role for user: {userId}");
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    IdentityUser user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        _logger.LogInformation($"User has successfully remove user: {userId} from role.");
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                _logger.LogInformation(error.Description, "From unhandled error checking");
                ModelState.AddModelError("", error.Description);
            }
                
        }
    }
}
