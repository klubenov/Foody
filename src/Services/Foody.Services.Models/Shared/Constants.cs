using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Shared
{
    public static class Constants
    {
        public const int NutritionItemNameMaxLength = 50;

        public const int NutritionItemNameMinLength = 3;

        public const string NutritionItemNameLengthErrorMessage = "The name should be between 3 and 50 characters.";

        public const string NameMissingErrorMessage = "You must enter a name.";

        public const int NutritionItemDescriptionMaxLength = 1000;

        public const int NutritionItemDescriptionMinLength = 20;

        public const string NutritionItemDescriptionLengthErrorMessage = "The description is too long or too short, it should be between 20 and 1000 characters.";

        public const double FoodItemMicroMaxContent = 5000d;

        public const string FoodItemMicroContentErrorMessage = "Micro elements' amount can range from 0 to 5000mg.";

        public const string FoodItemMacroContentErrorMessage = "Macro elements' amount can range from 0 to 100g.";

        public const int ArticleTitleMinLength = 20;

        public const int ArticleTitleMaxLength = 100;

        public const int ArticleContentMinLength = 100;

        public const int ArticleContentMaxLength = 10000;

        public const string ArticleTitleLengthErrorMessage = "The article's title should be between 20 and 100 characters.";

        public const string ArticleContentLengthErrorMessage = "The article's body should be between 100 and 10000 characters.";

        public const string ArticleTitleMissingErrorMessage = "The article should have a title.";

        public const string ArticleContentMissingErrorMessage = "The article should have a body.";
    }
}
