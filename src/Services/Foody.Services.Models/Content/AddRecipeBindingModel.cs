using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Foody.Services.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Content
{
    public class AddRecipeBindingModel
    {
        public AddRecipeBindingModel()
        {
            this.FoodItems = new List<FoodItemInRecipeBindingModel>();
        }

        [Required]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public List<FoodItemInRecipeBindingModel> FoodItems { get; set; }

        [Range(0, 10, ErrorMessage = "Cannot add more than 10 fields at once.")]
        public int AddFieldsCount { get; set; }

        public class FoodItemInRecipeBindingModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [Range(0d, 3000d)]
            public double AmountInGrams { get; set; }
        }
    }
}
