using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Foody.Services.DataServices.Models.Shared;
using Foody.Services.DataServices.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.DataServices.Models.Content
{
    public class AddFoodItemBindingModel
    {
        [Required(ErrorMessage = Constants.NameMissingErrorMessage)]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        [UniqueFoodItemName]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public List<MicroElementBindingModel> MicroElements { get; set; }

        [Required]
        public List<MacroElementBindingModel> MacroElements { get; set; }

        public class MicroElementBindingModel
        {
            [Required]
            public string Name { get; set; }

            [Range(0d, Constants.FoodItemMicroMaxContent, ErrorMessage = Constants.FoodItemMicroContentErrorMessage)]
            public double AmountInMilligramsPer100Grams { get; set; }
        }

        public class MacroElementBindingModel
        {
            [Required]
            public string Name { get; set; }

            [Range(0d, 100d, ErrorMessage = Constants.FoodItemMacroContentErrorMessage)]
            public double AmountInGramsPer100Grams { get; set; }
        }
    }
}
