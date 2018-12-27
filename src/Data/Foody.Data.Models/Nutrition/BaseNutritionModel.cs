using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foody.Data.Models.Nutrition
{
    public abstract class BaseNutritionModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageLocation { get; set; }
    }
}
