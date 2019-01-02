using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Content
{
    public class EditRecipeListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int IngredientsCount { get; set; }

        public double CaloriesPer100grams { get; set; }
    }
}
