using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Foody.Services.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Content
{
    public class EditMacroElementViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = Constants.MacroElementCaloricContentMissingErrorMessage)]
        [Range(Constants.MacroElementMinCaloricContent, Constants.MacroElementMaxCaloricContent, ErrorMessage = Constants.MacroElementCaloricContentRangeErrorMessage)]
        public double CaloricContentPerGram { get; set; }

        public IFormFile NewImage { get; set; }

        public string ImageLocation { get; set; }

        public string CurrentPage { get; set; }

        public string SearchText { get; set; }
    }
}
