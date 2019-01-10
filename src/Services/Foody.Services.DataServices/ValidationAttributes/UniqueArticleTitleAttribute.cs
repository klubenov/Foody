using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Foody.Data;

namespace Foody.Services.DataServices.ValidationAttributes
{
    public class UniqueArticleTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var title = (string) value;

            var context = (FoodyDbContext)validationContext.GetService(typeof(FoodyDbContext));

            if (context.Articles.Any(a => a.Title == title))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Article with such title already exists, please choose another one.";
        }
    }
}
