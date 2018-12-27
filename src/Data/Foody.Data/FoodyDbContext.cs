using Foody.Data.Models;
using Foody.Data.Models.FoodDiary;
using Foody.Data.Models.Nutrition;
using Foody.Data.Models.Publishing;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Foody.Data
{
    public class FoodyDbContext : IdentityDbContext<FoodyUser>
    {
        public FoodyDbContext(DbContextOptions<FoodyDbContext> options)
            : base(options)
        {
        }

        public DbSet<MicroElement> MicroElements { get; set; }
        public DbSet<MacroElement> MacroElements { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<FoodItemMicroElement> FoodItemMicroElements { get; set; }
        public DbSet<FoodItemMacroElement> FoodItemMacroElements { get; set; }
        public DbSet<RecipeFoodItem> RecipeFoodItems { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<MealFoodItem> MealFoodItems { get; set; }
        public DbSet<MealRecipe> MealRecipes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FoodItemMicroElement>().HasKey(pk => new {pk.FoodItemId, pk.MicroElementId});

            builder.Entity<Article>().HasIndex(a => a.Title).IsUnique();
            builder.Entity<MicroElement>().HasIndex(me => me.Name).IsUnique();
            builder.Entity<MacroElement>().HasIndex(me => me.Name).IsUnique();
            builder.Entity<Recipe>().HasIndex(r => r.Name).IsUnique();
            builder.Entity<FoodItem>().HasIndex(fi => fi.Name).IsUnique();


            builder.Entity<FoodItemMicroElement>()
                .HasOne(fime => fime.FoodItem)
                .WithMany(fi => fi.FoodItemMicroElements)
                .HasForeignKey(fime => fime.FoodItemId);

            builder.Entity<FoodItemMicroElement>()
                .HasOne(fime => fime.MicroElement)
                .WithMany(me => me.FoodItemMicroElements)
                .HasForeignKey(fime => fime.MicroElementId);

            builder.Entity<FoodItemMacroElement>().HasKey(pk => new {pk.FoodItemId, pk.MacroElementId});

            builder.Entity<FoodItemMacroElement>()
                .HasOne(fime => fime.FoodItem)
                .WithMany(fi => fi.FoodItemMacroElements)
                .HasForeignKey(fime => fime.FoodItemId);

            builder.Entity<FoodItemMacroElement>()
                .HasOne(fime => fime.MacroElement)
                .WithMany(me => me.FoodItemMacroElements)
                .HasForeignKey(fime => fime.MacroElementId);

            builder.Entity<RecipeFoodItem>().HasKey(pk => new {pk.RecipeId, pk.FoodItemId});

            builder.Entity<RecipeFoodItem>()
                .HasOne(rfi => rfi.Recipe)
                .WithMany(r => r.RecipeFoodItems)
                .HasForeignKey(rfi => rfi.RecipeId);

            builder.Entity<RecipeFoodItem>()
                .HasOne(rfi => rfi.FoodItem)
                .WithMany(fi => fi.RecipeFoodItems)
                .HasForeignKey(rfi => rfi.FoodItemId);

            builder.Entity<MealFoodItem>().HasKey(pk => new {pk.MealId, pk.FoodItemId});

            builder.Entity<MealFoodItem>()
                .HasOne(mfi => mfi.Meal)
                .WithMany(m => m.MealFoodItems)
                .HasForeignKey(mfi => mfi.MealId);

            builder.Entity<MealFoodItem>()
                .HasOne(mfi => mfi.FoodItem)
                .WithMany(fi => fi.MealFoodItems)
                .HasForeignKey(mfi => mfi.FoodItemId);

            builder.Entity<MealRecipe>().HasKey(pk => new {pk.MealId, pk.RecipeId});

            builder.Entity<MealRecipe>()
                .HasOne(mr => mr.Meal)
                .WithMany(m => m.MealRecipes)
                .HasForeignKey(mr => mr.MealId);

            builder.Entity<MealRecipe>()
                .HasOne(mr => mr.Recipe)
                .WithMany(r => r.MealRecipes)
                .HasForeignKey(mr => mr.RecipeId);
        }
    }
}
