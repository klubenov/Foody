using System;
using System.Collections.Generic;
using Foody.Data.Models.Nutrition;

namespace Foody.Data.Models.FoodDiary
{
    public class Meal : BaseModel
    {
        public Meal()
        {
            this.MealRecipes = new HashSet<MealRecipe>();
            this.MealFoodItems = new HashSet<MealFoodItem>();
        }

        public string Note { get; set; }

        public DateTime TimeOfConsumption { get; set; }

        public string FoodyUserId { get; set; }
        public FoodyUser FoodyUser { get; set; }

        public string LocationId { get; set; }
        public Location Location { get; set; }

        public virtual ICollection<MealRecipe> MealRecipes { get; set; }

        public virtual ICollection<MealFoodItem> MealFoodItems { get; set; }
    }
}
