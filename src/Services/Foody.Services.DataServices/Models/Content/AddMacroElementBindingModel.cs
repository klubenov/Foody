using System.ComponentModel.DataAnnotations;
using Foody.Services.DataServices.Models.Shared;
using Foody.Services.DataServices.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.DataServices.Models.Content
{
    public class AddMacroElementBindingModel
    {
        [Required(ErrorMessage = Constants.NameMissingErrorMessage)]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        [UniqueMacroElementName]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = Constants.MacroElementCaloricContentMissingErrorMessage)]
        [Range(Constants.MacroElementMinCaloricContent, Constants.MacroElementMaxCaloricContent, ErrorMessage = Constants.MacroElementCaloricContentRangeErrorMessage)]
        public double CaloricContentPerGram { get; set; }

        public IFormFile Image { get; set; }
    }
}
