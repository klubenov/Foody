using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Foody.Services.DataServices.Models.Diary;

namespace Foody.Services.DataServices.ValidationAttributes
{
    public class UniqueFoodItemInEditMealAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var foodItems = (IEnumerable<EditMealViewModel.FoodItemInEditMealViewModel>)value;
            var foodItemsNames = foodItems.Select(r => r.Name);

            if (foodItemsNames.Distinct().Count() < foodItems.Count())
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Please choose a certain food item only once.";
        }
    }
}
