using System;
using System.Collections.Generic;
using System.Text;
using Foody.Data.Models.Nutrition;

namespace Foody.Data.Models.FoodDiary
{
    public class MealRecipe
    {
        public string MealId { get; set; }
        public Meal Meal { get; set; }

        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public double AmountInGrams { get; set; }
    }
}
