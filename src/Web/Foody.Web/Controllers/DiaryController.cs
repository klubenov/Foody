using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Content;
using Foody.Services.DataServices.Diary;
using Foody.Services.DataServices.Models.Diary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Controllers
{
    public class DiaryController : Controller
    {
        private readonly IContentService contentService;
        private readonly IDiaryService diaryService;
        private readonly IPaginationService paginationService;

        public DiaryController(IContentService contentService, IDiaryService diaryService, IPaginationService paginationService)
        {
            this.contentService = contentService;
            this.diaryService = diaryService;
            this.paginationService = paginationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Identity/Account/Login?returnUrl=/Diary/Index");
            }

            var model = this.diaryService.GetIndexModel(this.User.Identity.Name);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DiaryIndexViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var customPeriodStatisticsModel =
                    this.diaryService.GetStatistics(model.StartCustomDate, model.EndCustomDate, this.User.Identity.Name);
                model.CustomPeriodStatistics = customPeriodStatisticsModel;
            }

            return View(model);
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
        public async Task<IActionResult> AddMeal(AddMealBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.diaryService.AddMeal(model, this.User.Identity.Name);
                return RedirectToAction("Index");
            }

            var foodItemNames = this.contentService.GetFoodItemsNames();
            var recipesNames = this.contentService.GetRecipesNames();

            this.ViewData["FoodItemsNames"] = foodItemNames.ToList();
            this.ViewData["RecipesNames"] = recipesNames.ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetMealsForEditing(DateTime startDateTime, DateTime endDateTime, int currentPage = 1, string searchText = null)
        {
            MealsListViewModel model = null;

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "from" + startDateTime.ToString() + "to" + endDateTime.ToString();
                model = this.diaryService.GetMealsListByPeriod(startDateTime, endDateTime, this.User.Identity.Name);
            }
            else
            {
                var datesStrings = searchText.Replace("from", string.Empty).Split("to").ToArray();
                var startDateFromPagination = DateTime.Parse(datesStrings[0]);
                var endDateFromPagination = DateTime.Parse(datesStrings[1]);
                model = this.diaryService.GetMealsListByPeriod(startDateFromPagination, endDateFromPagination, this.User.Identity.Name);
            }


            model = this.paginationService.GetPageModel<MealsListViewModel, MealForEditingListViewModel>(model,
                currentPage, searchText, this.GetType(), "GetMealsForEditing", null);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult OpenMealForEditing(string mealId, int currentPage, string searchText)
        {
            var meal = this.diaryService.GetMealForEditing(mealId);

            if (meal == null)
            {
                return RedirectToAction("GetMealsForEditing", new { currentPage, searchText });
            }

            meal.CurrentPage = currentPage;
            meal.SearchText = searchText;

            var foodItemNames = this.contentService.GetFoodItemsNames();
            var recipesNames = this.contentService.GetRecipesNames();

            this.ViewData["FoodItemsNames"] = foodItemNames.ToList();
            this.ViewData["RecipesNames"] = recipesNames.ToList();

            return View(meal);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMeal(EditMealViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.diaryService.EditMeal(model);
                return RedirectToAction("GetMealsForEditing", new { model.CurrentPage, model.SearchText });
            }

            var foodItemNames = this.contentService.GetFoodItemsNames();
            var recipesNames = this.contentService.GetRecipesNames();

            this.ViewData["FoodItemsNames"] = foodItemNames.ToList();
            this.ViewData["RecipesNames"] = recipesNames.ToList();

            return View("OpenMealForEditing", model);
        }
    }
}