using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Foody.Services.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Content
{
    public class EditRecipeViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        public IFormFile NewImage { get; set; }

        public string ImageLocation { get; set; }

        public string CurrentPage { get; set; }

        public string SearchText { get; set; }

        public List<FoodItemInRecipeViewModel> FoodItems { get; set; }

        [Range(0, 3, ErrorMessage = "Cannot add more than 3 fields at once.")]
        public int AddFieldsCount { get; set; }

        public class FoodItemInRecipeViewModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [Range(0d, 3000d)]
            public double AmountInGrams { get; set; }
        }
    }
}
