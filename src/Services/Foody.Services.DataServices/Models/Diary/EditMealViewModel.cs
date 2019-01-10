using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foody.Services.DataServices.ValidationAttributes;

namespace Foody.Services.DataServices.Models.Diary
{
    public class EditMealViewModel
    {
        [Required]
        public string Id { get; set; }

        [StringLength(50, ErrorMessage = "Location name can be maximum 50 characters.")]
        public string Location { get; set; }

        [StringLength(100, ErrorMessage = "Not too long, max 100 characters.")]
        public string Note { get; set; }

        [Required]
        public DateTime TimeOfConsumption { get; set; }

        public string SearchText { get; set; }

        public int CurrentPage { get; set; }

        [UniqueRecipeInEditMeal]
        public List<EditMealViewModel.RecipeInEditMealViewModel> Recipes { get; set; }

        [UniqueFoodItemInEditMeal]
        public List<EditMealViewModel.FoodItemInEditMealViewModel> FoodItems { get; set; }

        public class RecipeInEditMealViewModel
        {
            public string Name { get; set; }

            [Range(0d, 3000d, ErrorMessage = "Maximum allowed amount per recipe item is 3kg.")]
            public double AmountInGrams { get; set; }
        }

        public class FoodItemInEditMealViewModel
        {
            [Required]
            public string Name { get; set; }

            [Range(0d, 3000d, ErrorMessage = "Maximum allowed amount per food item is 3kg.")]
            public double AmountInGrams { get; set; }
        }
    }
}
