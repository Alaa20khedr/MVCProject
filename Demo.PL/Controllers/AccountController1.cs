﻿using Deme.DAL.Entities;
using Deme.DAL.Migrations;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController1 : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
		private readonly ILogger<AccountController1> logger;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController1(UserManager<ApplicationUser> userManager 
            , ILogger<AccountController1> logger
            ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
			this.logger = logger;
			this.signInManager = signInManager;
		}
        public IActionResult SignUp()
        {
            return View(new SignupViewModel());

        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignupViewModel input)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = input.Email,
                    UserName = input.Email.Split("@")[0],
                    IsActive = true
                };
                var result = await userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)

                    return RedirectToAction("Login");
                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                    ModelState.AddModelError("", error.Description);
                }
                
            }
          return View(input);
        }
		public IActionResult Login()
		{
			return View(new SignInViewModel());

		}
        [HttpPost]
		public async Task<IActionResult> Login(SignInViewModel input)
		{
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(input.Email);
                if (user is null)
                    ModelState.AddModelError("", "Email is not Exist");
                   
                
                if (user != null && await userManager.CheckPasswordAsync(user, input.Password))
                {
                    var result=await signInManager.PasswordSignInAsync(user,input.Password,input.RememberMe,false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");

				}
            }
            return View(input);

		}
		public async Task<IActionResult> LogOut()
        {
            await signInManager .SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult ForgetPassword()
        {
            return View(new ForgetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(input.Email);
                if (user is null)
                    ModelState.AddModelError("", "Email is not Exist");
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var ResetPasswordLink = Url.Action("ResetPassword", "AccountController1", new { Email = input.Email, Token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = ResetPasswordLink,
                        To = input.Email

                    };
                    EmailSetting.SendEmail(email);
                    return RedirectToAction("CompleteForgetPassword");

				}
		

			}
			return View(input);
		}

        public IActionResult ResetPassword(string email , string token)
        {
            return View(new ResetPasswordViewModel());
        }
        [HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if(ModelState.IsValid)
            {
				var user = await userManager.FindByEmailAsync(input.Email);
				if (user is null)
					ModelState.AddModelError("", "Email is not Exist");
                if(user != null)
                {
                    var result=await userManager.ResetPasswordAsync(user,input.Token ,input.Password);
					if (result.Succeeded)

						return RedirectToAction("Login");
					foreach (var error in result.Errors)
					{
						logger.LogError(error.Description);
						ModelState.AddModelError("", error.Description);
					}
				}
            
			}
			return View(input);
		}
       public IActionResult AccessDenied()
        {
            return View();
        }

	}
}