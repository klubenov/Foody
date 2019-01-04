using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Knowledge
{
    public class KnowledgeItemViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageLocation { get; set; }

        public string MicroElementType { get; set; }

        public double MacroElementCaloricContent { get; set; }

        public double RecipeCaloricContentPer100Grams { get; set; }

        public bool IsMicroElement { get; set; }

        public bool IsMacroElement { get; set; }

        public bool IsFoodItem { get; set; }

        public bool IsRecipe { get; set; }

        public string CurrentPage { get; set; }

        public string SearchText { get; set; }

        public List<MicroElementInFoodItemViewModel> MicroElementInFoodItemViewModels { get; set; }

        public List<MacroElementInFoodItemViewModel> MacroElementInFoodItemViewModels { get; set; }

        public List<FoodItemInRecipeViewModel> FoodItemInRecipeViewModels { get; set; }

        public class MicroElementInFoodItemViewModel
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public double Amount { get; set; }
        }

        public class MacroElementInFoodItemViewModel
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public double Amount { get; set; }
        }

        public class FoodItemInRecipeViewModel
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public double Amount { get; set; }
        }
    }
}
