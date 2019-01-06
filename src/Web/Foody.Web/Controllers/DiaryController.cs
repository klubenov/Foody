using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices.Content;
using Foody.Services.Models.Diary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Controllers
{
    public class DiaryController : Controller
    {
        private readonly IContentService contentService;

        public DiaryController(IContentService contentService)
        {
            this.contentService = contentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Identity/Account/Login?returnUrl=/Diary/Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddMeal()
        {
            var foodItemNames = this.contentService.GetFoodItemsNames();
            var recipesNames = this.contentService.GetRecipesNames();

            this.ViewData["FoodItemsNames"] = foodItemNames.ToList();
            this.ViewData["RecipesNames"] = recipesNames.ToList();

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult AddMeal(AddMealBindingModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var foodItemNames = this.contentService.GetFoodItemsNames();
                var recipesNames = this.contentService.GetRecipesNames();

                this.ViewData["FoodItemsNames"] = foodItemNames.ToList();
                this.ViewData["RecipesNames"] = recipesNames.ToList();

                return View(model);
            }

            return null;
        }
    }
}