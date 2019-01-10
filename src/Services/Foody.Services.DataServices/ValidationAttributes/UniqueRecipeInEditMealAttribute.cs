using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Foody.Services.DataServices.Models.Diary;

namespace Foody.Services.DataServices.ValidationAttributes
{
    public class UniqueRecipeInEditMealAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var recipes = (IEnumerable<EditMealViewModel.RecipeInEditMealViewModel>)value;
            var recipesNames = recipes.Select(r => r.Name);

            if (recipesNames.Distinct().Count() < recipes.Count())
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Please choose a certain recipe only once.";
        }
    }
}
