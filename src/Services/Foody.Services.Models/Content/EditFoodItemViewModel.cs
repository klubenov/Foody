using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Foody.Services.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Content
{
    public class EditFoodItemViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = Constants.NameMissingErrorMessage)]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        public IFormFile NewImage { get; set; }

        [Required]
        public List<MicroElementViewModel> MicroElements { get; set; }

        [Required]
        public List<MacroElementViewModel> MacroElements { get; set; }

        public string ImageLocation { get; set; }

        public string CurrentPage { get; set; }

        public string SearchText { get; set; }

        public class MicroElementViewModel
        {
            [Required]
            public string Name { get; set; }

            [Range(0d, Constants.FoodItemMicroMaxContent, ErrorMessage = Constants.FoodItemMicroContentErrorMessage)]
            public double AmountInMilligramsPer100Grams { get; set; }
        }

        public class MacroElementViewModel
        {
            [Required]
            public string Name { get; set; }

            [Range(0d, 100d, ErrorMessage = Constants.FoodItemMacroContentErrorMessage)]
            public double AmountInGramsPer100Grams { get; set; }
        }
    }
}
