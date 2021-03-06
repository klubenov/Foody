﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foody.Data.Models.FoodDiary
{
    public class Location : BaseModel
    {
        public Location()
        {
            this.Meals = new HashSet<Meal>();
        }

        [Required]
        public string Name { get; set; }

        public string FoodyUserId { get; set; }
        public FoodyUser FoodyUser { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}
