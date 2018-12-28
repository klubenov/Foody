using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices.Common;
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
        private const string InitialOpenCheckString = "TruestOfTheTrue";

        private readonly IMenuService menuService;
        private readonly IContentService contentService;
        private readonly IPaginationService paginationService;

        public ContentController(IMenuService menuService, IContentService contentService, IPaginationService paginationService)
        {
            this.menuService = menuService;
            this.contentService = contentService;
            this.paginationService = paginationService;
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

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Edit_Food_Items(int currentPage = 1, string searchText = null, string initialOpen = InitialOpenCheckString)
        {
            var model = this.contentService.GetAllEditFoodItemsForEditing(searchText);

            model.SearchText = searchText;

            model = this.paginationService.GetPageModel<AllEditFoodItemsViewModel, EditFoodItemListViewModel>(model,
                currentPage, this.GetType(), "Edit_Food_Items", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("Edit_Food_Items", model);
            }
            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Edit_Food_Items", model);
                return View("ContentMenu", reloadModel);
            }

            return RedirectToAction("ContentMenu");
        }

        public IActionResult OpenFoodItemForEditing(string foodItemId, string currentPage, string searchText)
        {
            var foodItem = this.contentService.GetFoodItemForEditing(foodItemId);

            if (foodItem == null)
            {
                return RedirectToAction("ContentMenu");
            }

            foodItem.CurrentPage = currentPage;
            foodItem.SearchText = searchText;

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenFoodItemForEditing", foodItem);

            return View("ContentMenu", reloadModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult EditFoodItem(EditFoodItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.contentService.EditFoodItem(model);
                return RedirectToAction("Edit_Food_Items",
                    new {currentPage = model.CurrentPage, searchText = model.SearchText, initialOpen = "false"});
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenFoodItemForEditing", model);
            return View("ContentMenu", reloadModel);
        }
    }
}