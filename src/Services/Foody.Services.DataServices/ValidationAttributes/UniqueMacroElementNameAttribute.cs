﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Foody.Data;

namespace Foody.Services.DataServices.ValidationAttributes
{
    public class UniqueMacroElementNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var name = (string)value;

            var context = (FoodyDbContext)validationContext.GetService(typeof(FoodyDbContext));

            if (context.MacroElements.Any(me => me.Name == name))
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Macro element with such name already exists, please choose another one.";
        }
    }
}
