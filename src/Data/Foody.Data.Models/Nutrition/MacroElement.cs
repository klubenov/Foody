using System.Collections.Generic;

namespace Foody.Data.Models.Nutrition
{
    public class MacroElement : BaseNutritionModel
    {
        public MacroElement()
        {
            this.FoodItemMacroElements = new HashSet<FoodItemMacroElement>();
        }

        public double CaloricContentPerGram { get; set; }

        public virtual ICollection<FoodItemMacroElement> FoodItemMacroElements { get; set; }
    }
}
