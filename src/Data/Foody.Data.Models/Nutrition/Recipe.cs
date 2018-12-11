using System.Collections.Generic;
using Foody.Data.Models.FoodDiary;

namespace Foody.Data.Models.Nutrition
{
    public class Recipe : BaseNutritionModel
    {
        public Recipe()
        {
            this.RecipeFoodItems = new HashSet<RecipeFoodItem>();
            this.MealRecipes = new HashSet<MealRecipe>();
        }

        public bool IsApproved { get; set; }

        public bool IsSentForApproval { get; set; }

        public bool IsRejected { get; set; }

        public string OwnerId { get; set; }
        public FoodyUser Owner { get; set; }

        public virtual ICollection<RecipeFoodItem> RecipeFoodItems { get; set; }

        public virtual ICollection<MealRecipe> MealRecipes { get; set; }
    }
}
