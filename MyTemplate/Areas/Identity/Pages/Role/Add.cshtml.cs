using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MyTemplate.Areas.Identity.Pages.Role
{
    public class AddModel : PageModel
    {
        private readonly ILogger<AddModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        [TempData]
        public string StatusMessage { set; get; }

        public class CreateModel
        {
            public string Id { set; get; }

            public string Name { set; get; }
        }

        [BindProperty]
        public bool IsUpdatePage { set; get; } = false;

        [BindProperty]
        public CreateModel Input { set; get; }
        public AddModel(ILogger<AddModel> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }
        public IActionResult OnPost() => NotFound("Not Found!");
        public IActionResult OnGet() => NotFound("Not Found!");

        //Hiển thị Page Add
        public IActionResult OnPostStartNewRole()
        {
            StatusMessage = "Type values of Role.. ";
            IsUpdatePage = false;
            ModelState.Clear();
            return Page();
        }
        public async Task<IActionResult> OnPostStartDeleteRole()
        {
            StatusMessage = string.Empty;
            var role = await _roleManager.FindByIdAsync(Input.Id);
            if(role != null)
            {

            }
            return Page();
        }
        public async Task<IActionResult> OnPostStartUpdateRole()
        {
            StatusMessage = string.Empty;
            var role = await _roleManager.FindByIdAsync(Input.Id);
            if(role != null)
            {
                IsUpdatePage = true;
                Input.Name = role.Name;
                ModelState.Clear();
            }
            else
            {
                StatusMessage = $"Error: Role = {Input.Id} Not Found!";
            }
            return Page();
        }
        public async Task<IActionResult> OnPostSave()
        {
            if(IsUpdatePage)
            {
                //Trường hợp update role
                if(Input.Id == null)
                {
                    ModelState.Clear();
                    StatusMessage = "Error: Role is not found!";
                    return Page();
                }
                else
                {
                    var role = await _roleManager.FindByIdAsync(Input.Id);
                    if (role != null)
                    {
                        role.Name = Input.Name;
                        //Save role to DB;
                        var savedRole = await _roleManager.UpdateAsync(role);
                        if(savedRole.Succeeded)
                        {
                            StatusMessage = "Update role successfully!";
                            return RedirectToPage("./Index");
                        }
                        else
                        {
                            StatusMessage = "Error!";
                            foreach (var i in savedRole.Errors)
                            {
                                StatusMessage += i.Description;
                            }
                        }
                    }

                    else
                    {
                        StatusMessage = $"Error: Role = {Input.Id} Not Found!";
                    }
                }
            }
            else
            {
                //Trường hợp create new role
            var role = new IdentityRole(Input.Name);
            var createdRole = await _roleManager.CreateAsync(role);
            if(createdRole.Succeeded)
            {
                StatusMessage = "Added new role successfully!";
                return RedirectToPage("./Index");
            }
            else
            {
                StatusMessage = "Error!";
                foreach(var i in createdRole.Errors)
                    {
                    StatusMessage += i.Description;
                    }
                }
            }
            return Page();
        }
    }
}
