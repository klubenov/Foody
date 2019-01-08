using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Diary
{
    public class StatisticsViewModel
    {
        public double AverageDailyCalories { get; set; }

        public string CaloricRatio { get; set; }

        public string MostUsedFoodItemId { get; set; }

        public string MostUsedFoodItem { get; set; }

        public string MostUsedRecipeId { get; set; }

        public string MostUsedRecipe { get; set; }
    }
}
