    using AniWatch.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    namespace AniWatch.Areas.Admin.Controllers
    {
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
        [Route("Admin/[controller]")]
        public class RoleController : Controller
        {
            private readonly RoleManager<IdentityRole> roleManager;
            private readonly UserManager<AppUser> userManager;


            public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
            {
                this.roleManager = roleManager;
                this.userManager = userManager;
            }

            [HttpGet("Index")]
            public IActionResult Index() => View(roleManager.Roles);

            [HttpGet("Create")]
            public IActionResult Create() => View();

            [HttpPost("Create")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([MinLength(2), Required] string name)
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole(name);
                    var result = await roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View();
            }

 
            [HttpGet("Edit")]
            public async Task<IActionResult> Edit(string id)
            {

 
                var role = await roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with id = {id} cannot be found";
                    return View("NotFound");
                }

                var usersInRole = await userManager.Users.ToListAsync();
                var model = new RoleEdit
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    Users = usersInRole.Select(user => user.UserName).ToList()
                };

                return View(model);
            }


            [HttpPost("Edit")]
            public async Task<IActionResult> Edit(RoleEdit model)
            {

                var role = await roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with id= {model.Id} cannt ne found";
                    return View("NotFound");
                }
                else
                {
                    role.Name = model.RoleName;
                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError("", error.Description);

                    }
                    return View(model);
                }

            }

            [HttpGet]
            public async Task<IActionResult> EditUserInRole(string roleId)
            {
                var role = await roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with id = {roleId} cannot be found";
                    return View("NotFound");
                }

                var usersInRole = await userManager.Users.ToListAsync();

                var model = usersInRole.Select(user => new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = true // Assuming all users in the role are selected by default
                }).ToList();



                return View(model);

            }
            [HttpPost]
            public async Task<IActionResult> EditUserInRole(List<UserViewModel> model, string roleId)
            {
                var role = await roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                    return View("NotFound");
                }
                for (int i = 0; i < model.Count; i++)
                {
                    var user = await userManager.FindByIdAsync(model[i].UserId);
                    IdentityResult result = null;
                    if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                    {
                        result = await userManager.AddToRoleAsync(user, role.Name);

                    }
                    else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    else
                    {
                        continue;
                    }
                    if (result.Succeeded)
                    {
                        if (i < (model.Count - 1))
                            continue;
                        else
                            return RedirectToAction("Index", new { id = roleId });
                    }


                }
                for (int i = 0; i < model.Count; i++)

                    return RedirectToAction("Index", new { id = roleId });
			    return View();
		    }




        public async Task<IActionResult> Delete(string id) 
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                TempData["Error"] = "The role has been deleted";
            }
            else 
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    TempData["Success"] = role.Name;
                }
                else { foreach (IdentityError error in result.Errors) { ModelState.AddModelError("", error.Description); } }
            }
            return RedirectToAction("Index");
        }







    }
}
