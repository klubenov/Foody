using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Diary
{
    public class MealForEditingListViewModel
    {
        public string MealId { get; set; }

        public DateTime TimeOfConsumption { get; set; }

        public string Location { get; set; }

        public string Note { get; set; }

        public double CaloriesFromProteins { get; set; }

        public double CaloriesFromCarbohydrates { get; set; }

        public double CaloriesFromFats { get; set; }

        public double TotalCalories { get; set; }
    }
}
