using System.ComponentModel.DataAnnotations;
using Foody.Services.DataServices.Models.Shared;
using Foody.Services.DataServices.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.DataServices.Models.Content
{
    public class AddMicroElementBindingModel
    {
        [Required(ErrorMessage = Constants.NameMissingErrorMessage)]
        [StringLength(Constants.NutritionItemNameMaxLength, MinimumLength = Constants.NutritionItemNameMinLength, ErrorMessage = Constants.NutritionItemNameLengthErrorMessage)]
        [UniqueMicroElementName]
        public string Name { get; set; }

        [StringLength(Constants.NutritionItemDescriptionMaxLength, MinimumLength = Constants.NutritionItemDescriptionMinLength, ErrorMessage = Constants.NutritionItemDescriptionLengthErrorMessage)]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must select type.")]
        [RegularExpression("^Vitamin$|^Mineral$|^Other$", ErrorMessage = "Invalid type.")]
        public string Type { get; set; }

        public IFormFile Image { get; set; }
    }
}
