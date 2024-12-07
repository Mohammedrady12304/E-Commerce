using E_Commerce1.Models;
using E_Commerce1.ViewModels;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUserRepository _applicationUserRepository;

        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager, IApplicationUserRepository applicationUserRepository)
        {
                _userManager =userManager;
                _signInManager = signInManager;
                _applicationUserRepository = applicationUserRepository;
        }
        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        
        [Authorize(Roles="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                //create Account
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.Address = newUserVM.Address;
                applicationUser.UserName = newUserVM.UserName;
                applicationUser.PasswordHash = newUserVM.Password;
                if (newUserVM.ProfilePicture != null) {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newUserVM.ProfilePicture.CopyToAsync(memoryStream);
                        applicationUser.ProfilePicture = memoryStream.ToArray();
                    }
                }
                   

                IdentityResult result = await _userManager.CreateAsync(applicationUser, newUserVM.Password);

                if (result.Succeeded == true)
                {
                    //assign to role
                    await _userManager.AddToRoleAsync(applicationUser, "Admin");
                    //create cookies
                    await _signInManager.SignInAsync(applicationUser, false);
                    return RedirectToAction(/*هنا هنحط ال view الاساسي   (action,controller)*/  "Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }

            return View(newUserVM);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                //check
              ApplicationUser userModel= await _userManager.FindByNameAsync(userVM.UserName);
                if(userModel != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found)
                    {
                        await _signInManager.SignInAsync(userModel, userVM.RemeberMe);
                        return RedirectToAction(/*هنا هنحط ال action بتاع ال view الرئيسي*/);
                    }
                }
                ModelState.AddModelError("", "UserName and Password is wrong");
            }
            return View(userVM);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel newUserVM)
        {
            if (ModelState.IsValid)
            {
                //create Account
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.Address = newUserVM.Address;
                applicationUser.UserName = newUserVM.UserName;
                applicationUser.PasswordHash = newUserVM.Password;
                if (newUserVM.ProfilePicture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newUserVM.ProfilePicture.CopyToAsync(memoryStream);
                        applicationUser.ProfilePicture = memoryStream.ToArray();
                    }
                }
                IdentityResult result=await _userManager.CreateAsync(applicationUser,newUserVM.Password);

                if (result.Succeeded==true)
                {
                    //create cookies
                    await _signInManager.SignInAsync(applicationUser,false);
                    return RedirectToAction(/*هنا هنحط ال view الاساسي   (action,controller)*/  "Index","Home");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            
            }    

            return View(newUserVM);
        }

        public async Task <IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }


        public async Task<IActionResult> GetProfilePicture()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.ProfilePicture == null)
            {
                return File("/images/default-profile.png", "image/png");
            }

            return File(user.ProfilePicture, "image/jpeg");
        }
    }
}

