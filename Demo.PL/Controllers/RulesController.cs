using Deme.DAL.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class RulesController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ILogger<RulesController> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public RulesController(RoleManager<ApplicationRole> roleManager, ILogger<RulesController> logger, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.logger = logger;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(role);
        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return NotFound();
            var user = await roleManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return View(viewName, user);

        }
        public async Task<IActionResult> Update(string id)
        {
            return await Details(id, "Update");

        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
                return NotFound();
            if (ModelState.IsValid)
            {

                var role = await roleManager.FindByIdAsync(id);
                role.Name = applicationRole.Name;
                role.NormalizedName = applicationRole.Name.ToUpper();
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)

                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);

                }

            }
            return View(applicationRole);
        }
        public async Task<IActionResult> Delete(string id)
        {



            var user = await roleManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var result = await roleManager.DeleteAsync(user);
            if (result.Succeeded)

                return RedirectToAction("Index");
            foreach (var error in result.Errors)
            {
                logger.LogError(error.Description);
                ModelState.AddModelError("", error.Description);

            }



            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();
            ViewBag.RoleId=roleId;
            var usersinrole = new List<UserInRoleViewModel>();

            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var userinrole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                    userinrole.IsSelected = true;
                else
                    userinrole.IsSelected = false;
                usersinrole.Add(userinrole);
            }
            return View(usersinrole);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId, List<UserInRoleViewModel> users)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await userManager.FindByIdAsync(user.UserId);
                    if(appUser != null)
                    {
                        if(user.IsSelected && !await userManager.IsInRoleAsync(appUser, role.Name))
                            await userManager.AddToRoleAsync(appUser, role.Name);
                        else if (!user.IsSelected && await userManager.IsInRoleAsync(appUser, role.Name))
                            await userManager.RemoveFromRoleAsync(appUser, role.Name);
                    }
                }
                return RedirectToAction(nameof(Update), new { id = roleId });
            }
            return View(users);
        }
    }
}