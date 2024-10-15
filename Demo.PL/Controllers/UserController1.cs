using Deme.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController1 : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<UserController1> logger;

        public UserController1(UserManager<ApplicationUser> userManager , ILogger<UserController1>logger)
		{
			this.userManager = userManager;
            this.logger = logger;
        }
		public async Task<IActionResult> Index(string searchValue="")
		{
			List<ApplicationUser> users;
			if (string.IsNullOrEmpty(searchValue))
			
				users=await userManager.Users.ToListAsync();
			else
			users=await userManager.Users.Where(user=>user.Email.Trim().ToLower().Contains(searchValue.Trim().ToLower())).ToListAsync();
			
		return View(users);
		}


		public async Task<IActionResult> Details(string id, string viewName = "Details")
		{
			if (id is null)
				return NotFound();
			var user=await userManager.FindByIdAsync(id);
			if(user == null)
				return NotFound();
			return View(viewName,user);	

		}
        public async Task<IActionResult> Update(string id)
		{
			return await Details( id, "Update");

		}
		[HttpPost]
        public async Task<IActionResult> Update(string id,ApplicationUser applicationUser)
		{ 
			if(id !=applicationUser.Id)
				return NotFound();
			if(ModelState.IsValid) { 

				var user=await userManager.FindByIdAsync(id);
				user.UserName = applicationUser.UserName;
				user.NormalizedUserName = applicationUser.UserName.ToUpper();
				var result=await userManager.UpdateAsync(user);
				if (result.Succeeded)
				
					return RedirectToAction("Index");
				foreach (var error in result.Errors)
				{
					logger.LogError(error.Description);
					ModelState.AddModelError("", error.Description);

				}
			
			}
            return View(applicationUser);
        }
        public async Task<IActionResult> Delete(string id)
        {
          
           

                var user = await userManager.FindByIdAsync(id);
               if(user == null) return NotFound();
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)

                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);

                }

            

            return RedirectToAction("Index");
        }
    }
}
