using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Content;
using Foody.Services.DataServices.Models.Content;
using Foody.Services.WebServices.Menu;
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
        public IActionResult Edit_Micro_Elements(int currentPage = 1, string searchText = null, string initialOpen = InitialOpenCheckString)
        {
            var model = this.contentService.GetAllMicroElementsForEditing(searchText);

            model.SearchText = searchText;

            model = this.paginationService.GetPageModel<AllEditMicroElementsViewModel, EditMicroElementListViewModel>(model,
                currentPage, searchText, this.GetType(), "Edit_Micro_Elements", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("Edit_Micro_Elements", model);
            }
            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Edit_Micro_Elements", model);
                return View("ContentMenu", reloadModel);
            }

            return RedirectToAction("ContentMenu");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult OpenMicroElementForEditing(string microElementId, string currentPage, string searchText)
        {
            var microElement = this.contentService.GetMicroElementForEditing(microElementId);

            if (microElement == null)
            {
                return RedirectToAction("ContentMenu");
            }

            microElement.CurrentPage = currentPage;
            microElement.SearchText = searchText;

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenMicroElementForEditing", microElement);

            return View("ContentMenu", reloadModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult EditMicroElement(EditMicroElementViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.contentService.EditMicroElement(model);
                return RedirectToAction("Edit_Micro_Elements",
                    new { currentPage = model.CurrentPage, searchText = model.SearchText, initialOpen = "false" });
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenMicroElementForEditing", model);
            return View("ContentMenu", reloadModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Add_Macro_Element()
        {
            return PartialView("Add_Macro_Element");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Add_Macro_Element(AddMacroElementBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.contentService.AddMacroElement(model);
                return RedirectToAction("ContentMenu");
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Add_Macro_Element", model);
            return View("ContentMenu", reloadModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Edit_Macro_Elements(int currentPage = 1, string searchText = null, string initialOpen = InitialOpenCheckString)
        {
            var model = this.contentService.GetAllMacroElementsForEditing(searchText);

            model.SearchText = searchText;

            model = this.paginationService.GetPageModel<AllEditMacroElementsViewModel, EditMacroElementListViewModel>(model,
                currentPage, searchText, this.GetType(), "Edit_Macro_Elements", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("Edit_Macro_Elements", model);
            }
            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Edit_Macro_Elements", model);
                return View("ContentMenu", reloadModel);
            }

            return RedirectToAction("ContentMenu");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult OpenMacroElementForEditing(string macroElementId, string currentPage, string searchText)
        {
            var macroElement = this.contentService.GetMacroElementForEditing(macroElementId);

            if (macroElement == null)
            {
                return RedirectToAction("ContentMenu");
            }

            macroElement.CurrentPage = currentPage;
            macroElement.SearchText = searchText;

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenMacroElementForEditing", macroElement);

            return View("ContentMenu", reloadModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult EditMacroElement(EditMacroElementViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.contentService.EditMacroElement(model);
                return RedirectToAction("Edit_Macro_Elements",
                    new { currentPage = model.CurrentPage, searchText = model.SearchText, initialOpen = "false" });
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenMacroElementForEditing", model);
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
                currentPage, searchText, this.GetType(), "Edit_Food_Items", typeof(AreaAttribute));

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

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Add_Recipe()
        {
            var foodItems = this.contentService.GetFoodItemsNames();

            this.ViewData["FoodItemsNames"] = foodItems.ToList();

            return PartialView("Add_Recipe");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Add_Recipe(AddRecipeBindingModel model)
        {
            var foodItems = this.contentService.GetFoodItemsNames();

            this.ViewData["FoodItemsNames"] = foodItems.ToList();

            if (this.ModelState.IsValid)
            {
                if (model.AddFieldsCount != 0)
                {
                    for (int i = 0; i < model.AddFieldsCount; i++)
                    {
                        var newField = new AddRecipeBindingModel.FoodItemInRecipeBindingModel();
                        model.FoodItems.Add(newField);
                    }

                    model.AddFieldsCount = 0;

                    var reloadModeWithNewFields = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Add_Recipe", model);
                    return View("ContentMenu", reloadModeWithNewFields);
                }

                this.contentService.AddRecipe(model);
                return RedirectToAction("ContentMenu");
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Add_Recipe", model);
            return View("ContentMenu", reloadModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Edit_Recipes(int currentPage = 1, string searchText = null, string initialOpen = InitialOpenCheckString)
        {
            var model = this.contentService.GetAllRecipesForEditing(searchText);

            model.SearchText = searchText;

            model = this.paginationService.GetPageModel<AllEditRecipesViewModel, EditRecipeListViewModel>(model,
                currentPage, searchText, this.GetType(), "Edit_Recipes", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("Edit_Recipes", model);
            }
            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Edit_Recipes", model);
                return View("ContentMenu", reloadModel);
            }

            return RedirectToAction("ContentMenu");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult OpenRecipeForEditing(string recipeId, string currentPage, string searchText)
        {
            var foodItems = this.contentService.GetFoodItemsNames();

            this.ViewData["FoodItemsNames"] = foodItems.ToList();

            var recipe = this.contentService.GetRecipeForEditing(recipeId);

            if (recipe == null)
            {
                return RedirectToAction("ContentMenu");
            }

            recipe.CurrentPage = currentPage;
            recipe.SearchText = searchText;

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenRecipeForEditing", recipe);

            return View("ContentMenu", reloadModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult EditRecipe(EditRecipeViewModel model)
        {
            var foodItems = this.contentService.GetFoodItemsNames();

            this.ViewData["FoodItemsNames"] = foodItems.ToList();

            if (this.ModelState.IsValid)
            {
                if (model.AddFieldsCount != 0)
                {
                    for (int i = 0; i < model.AddFieldsCount; i++)
                    {
                        var newField = new EditRecipeViewModel.FoodItemInRecipeViewModel();
                        model.FoodItems.Add(newField);
                    }

                    model.AddFieldsCount = 0;

                    var reloadModeWithNewFields = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenRecipeForEditing", model);
                    return View("ContentMenu", reloadModeWithNewFields);
                }

                this.contentService.EditRecipe(model);
                return RedirectToAction("Edit_Recipes",
                    new { currentPage = model.CurrentPage, searchText = model.SearchText, initialOpen = "false" });
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenRecipeForEditing", model);
            return View("ContentMenu", reloadModel);
        }
    }
}