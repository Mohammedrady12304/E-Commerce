using E_Commerce1.Models;
using E_Commerce1.ViewModels;
using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager , SignInManager<ApplicationUser> _signInManager)
        {
                userManager = _userManager;
                signInManager = _signInManager;
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
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
                   

                IdentityResult result = await userManager.CreateAsync(applicationUser, newUserVM.Password);

                if (result.Succeeded == true)
                {
                    //assign to role
                    await userManager.AddToRoleAsync(applicationUser, "Admin");
                    //create cookies
                    await signInManager.SignInAsync(applicationUser, false);
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
              ApplicationUser userModel= await userManager.FindByNameAsync(userVM.UserName);
                if(userModel != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(userModel, userVM.RemeberMe);
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
                IdentityResult result=await userManager.CreateAsync(applicationUser,newUserVM.Password);

                if (result.Succeeded==true)
                {
                    //create cookies
                    await signInManager.SignInAsync(applicationUser,false);
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
            await signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}
