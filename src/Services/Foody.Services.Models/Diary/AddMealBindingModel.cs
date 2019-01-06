using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foody.Services.Models.Diary
{
    public class AddMealBindingModel
    {
        [StringLength(50, ErrorMessage = "Location name can be maximum 50 characters.")]
        public string Location { get; set; }

        [StringLength(100, ErrorMessage = "Not too long, max 100 characters.")]
        public string Note { get; set; }

        [Required]
        public DateTime TimeOfConsumption { get; set; }

        public List<RecipeInMealViewModel> Recipes { get; set; }

        public List<FoodItemInMealViewModel> FoodItems { get; set; }

        public class RecipeInMealViewModel
        {
            public string Name { get; set; }

            [Range(0d, 3000d, ErrorMessage = "Maximum allowed amount per recipe item is 3kg.")]
            public double AmountInGrams { get; set; }
        }

        public class FoodItemInMealViewModel
        {
            [Required]
            public string Name { get; set; }

            [Range(0d, 3000d, ErrorMessage = "Maximum allowed amount per food item is 3kg.")]
            public double AmountInGrams { get; set; }
        }
    }
}
