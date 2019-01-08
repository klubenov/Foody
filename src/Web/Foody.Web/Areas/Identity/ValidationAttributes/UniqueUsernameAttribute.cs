using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Foody.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Foody.Web.Areas.Identity.ValidationAttributes
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
       protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string username = (string) value;
            var context = (FoodyDbContext)validationContext.GetService(typeof(FoodyDbContext));

            if (context.Users.Any(u => u.UserName == username))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Username already taken, please choose another one.";
        }
    }
}
