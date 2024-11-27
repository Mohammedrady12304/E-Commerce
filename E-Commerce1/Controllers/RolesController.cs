using E_Commerce1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce1.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RolesController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager = _roleManager;   
        }
        //creation action
        //link 
        [HttpGet]
      public IActionResult New()
        {
            return View();
        }
        //submit
        [HttpPost]
      public async Task<IActionResult> New(RoleViewModel roleViewModel)
        {

            if (ModelState.IsValid)
            {
                //1-recive role name from client
                IdentityRole role = new IdentityRole();
                role.Name = roleViewModel.RoleName;

                //2-create the role 
                IdentityResult result = await roleManager.CreateAsync(role);

                if (result.Succeeded) {
                    return View(new RoleViewModel());
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(roleViewModel);
        }
    }
}
