using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Data.Models.Nutrition
{
    public abstract class BaseNutritionModel : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageLocation { get; set; }
    }
}
