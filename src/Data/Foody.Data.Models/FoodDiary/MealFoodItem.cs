using System;
using System.Collections.Generic;
using System.Text;
using Foody.Data.Models.Nutrition;

namespace Foody.Data.Models.FoodDiary
{
    public class MealFoodItem
    {
        public string MealId { get; set; }
        public Meal Meal { get; set; }

        public string FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public double AmountInGrams { get; set; }
    }
}
