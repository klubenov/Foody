namespace Foody.Data.Models.Nutrition
{
    public class FoodItemMacroElement
    {
        public string FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public string MacroElementId { get; set; }
        public MacroElement MacroElement { get; set; }

        public double AmountInGrams { get; set; }
    }
}
