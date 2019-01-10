using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices;
using Foody.Services.DataServices.Users;
using Foody.Services.WebServices.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Foody.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class UsersController : Controller
    {
        private readonly IMenuService menuService;
        private readonly IUsersService usersService;

        public UsersController(IMenuService menuService, IUsersService usersService)
        {
            this.menuService = menuService;
            this.usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles = "Super-admin")]
        public IActionResult UsersMenu(string selectedPartial = null, dynamic selectedPartialModel = null)
        {
            var model = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), "Administration", selectedPartial, selectedPartialModel);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Super-admin")]
        public IActionResult Roles()
        {
            var model = this.usersService.GetAllUsersWithRoles();

            return PartialView("Roles", model);
        }

        [HttpPost]
        [Authorize(Roles = "Super-admin")]
        public IActionResult ChangeRole(string username, string role)
        {
            this.usersService.ChangeRole(username, role);

            return RedirectToAction("UsersMenu", new { selectedPartial = "Roles"});
        }
    }
}