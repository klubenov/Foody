using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices.Content;
using Foody.Services.DataServices.Menu;
using Foody.Services.Models.Content;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Areas.Administration.Controllers
{
    [Area(AreaName)]
    public class ContentController : Controller
    {
        private const string AreaName = "Administration";

        private readonly IMenuService menuService;
        private readonly IContentService contentService;

        public ContentController(IMenuService menuService, IContentService contentService)
        {
            this.menuService = menuService;
            this.contentService = contentService;
        }

        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult ContentMenu()
        {
            var model = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, null, null);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Add_Micro_Element()
        {
            return PartialView("Add_Micro_Element");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Add_Micro_Element(AddMicroElementBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.contentService.AddMicroElement(model);
                return RedirectToAction("ContentMenu");
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Add_Micro_Element", model);
            return View("ContentMenu", reloadModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Add_Food_Item()
        {
            var microElementsNames = this.contentService.GetMicroElementsNames();
            var macroElementsNames = this.contentService.GetMacroElementsNames();

            this.ViewData["MicroElementsNames"] = microElementsNames.ToList();
            this.ViewData["MacroElementsNames"] = macroElementsNames.ToList();

            return PartialView("Add_Food_Item");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Add_Food_Item(AddFoodItemBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.contentService.AddFoodItem(model);
                return RedirectToAction("ContentMenu");
            }
            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Add_Food_Item", model);
            return View("ContentMenu", reloadModel);
        }
    }
}