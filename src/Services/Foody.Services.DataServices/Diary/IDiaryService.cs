﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Foody.Data.Models.FoodDiary;
using Foody.Services.DataServices.Models.Diary;

namespace Foody.Services.DataServices.Diary
{
    public interface IDiaryService
    {
        Task<Meal> AddMeal(AddMealBindingModel model, string username);

        MealsListViewModel GetMealsListByPeriod(DateTime startDateTime, DateTime endDateTime, string username);

        EditMealViewModel GetMealForEditing(string mealId);

        Task<Meal> EditMeal(EditMealViewModel model);

        DiaryIndexViewModel GetIndexModel(string username);

        StatisticsViewModel GetStatistics(DateTime modelStartCustomDate, DateTime modelEndCustomDate, string username);
    }
}
