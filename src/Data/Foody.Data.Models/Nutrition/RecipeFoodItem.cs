namespace Foody.Data.Models.Nutrition
{
    public class RecipeFoodItem
    {
        public string RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public double AmountInGrams { get; set; }
    }
}
