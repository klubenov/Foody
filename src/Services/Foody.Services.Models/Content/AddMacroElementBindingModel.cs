﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Foody.Services.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Content
{
    public class AddMacroElementBindingModel
    {
        [Required]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = Constants.MacroElementCaloricContentMissingErrorMessage)]
        [Range(Constants.MacroElementMinCaloricContent, Constants.MacroElementMaxCaloricContent, ErrorMessage = Constants.MacroElementCaloricContentRangeErrorMessage)]
        public double CaloricContentPerGram { get; set; }

        public IFormFile Image { get; set; }
    }
}