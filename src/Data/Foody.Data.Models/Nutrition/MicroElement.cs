using System.Collections.Generic;

namespace Foody.Data.Models.Nutrition
{
    public class MicroElement : BaseNutritionModel
    {
        public MicroElement()
        {
            this.FoodItemMicroElements = new HashSet<FoodItemMicroElement>();
        }

        public virtual ICollection<FoodItemMicroElement> FoodItemMicroElements { get; set; }
    }
}
