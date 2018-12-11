using System.Collections.Generic;
using Foody.Data.Models.FoodDiary;

namespace Foody.Data.Models.Nutrition
{
    public class FoodItem : BaseNutritionModel
    {
        public FoodItem()
        {
            this.FoodItemMicroElements = new List<FoodItemMicroElement>();
            this.FoodItemMacroElements = new HashSet<FoodItemMacroElement>();
            this.RecipeFoodItems = new HashSet<RecipeFoodItem>();
            this.MealFoodItems = new HashSet<MealFoodItem>();
        }

        public virtual ICollection<FoodItemMacroElement> FoodItemMacroElements { get; set; }

        public virtual ICollection<FoodItemMicroElement> FoodItemMicroElements { get; set; }

        public virtual ICollection<RecipeFoodItem> RecipeFoodItems { get; set; }

        public virtual ICollection<MealFoodItem> MealFoodItems { get; set; }
    }
}
