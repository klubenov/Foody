using System.Collections.Generic;
using Foody.Data.Models.FoodDiary;
using Foody.Data.Models.Nutrition;
using Foody.Data.Models.Publishing;
using Microsoft.AspNetCore.Identity;

namespace Foody.Data.Models
{
    // Add profile data for application users by adding properties to the FoodyUser class
    public class FoodyUser : IdentityUser
    {
        public FoodyUser()
        {
            this.Meals = new HashSet<Meal>();
            this.Locations = new HashSet<Location>();
            this.Articles = new HashSet<Article>();
            this.Comments = new HashSet<Comment>();
        }

        public virtual ICollection<Meal> Meals { get; set; }

        public virtual ICollection<Location> Locations { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
    }
}
