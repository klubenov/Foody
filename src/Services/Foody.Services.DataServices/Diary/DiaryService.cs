using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Foody.Data;
using Foody.Data.Models.FoodDiary;
using Foody.Data.Models.Nutrition;
using Foody.Services.DataServices.Common;
using Foody.Services.Models.Diary;
using Microsoft.EntityFrameworkCore;

namespace Foody.Services.DataServices.Diary
{
    public class DiaryService : IDiaryService
    {
        private readonly FoodyDbContext context;
        private readonly IPaginationService paginationService;

        public DiaryService(FoodyDbContext context, IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public async Task<Meal> AddMeal(AddMealBindingModel model, string username)
        {
            var user = this.context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            Location location = null;

            if (!string.IsNullOrEmpty(model.Location))
            {
                location = this.HandleLocation(model.Location, user.Id);
            }

            var mealFoodItems = new List<MealFoodItem>();
            var mealRecipes = new List<MealRecipe>();

            if (model.FoodItems != null)
            {
                foreach (var foodItemBindingModel in model.FoodItems)
                {
                    var foodItem = this.context.FoodItems.FirstOrDefault(fi => fi.Name == foodItemBindingModel.Name);
                    if (foodItem != null && foodItemBindingModel.AmountInGrams > 0)
                    {
                        var mealFoodItem = new MealFoodItem
                        {
                            FoodItem = foodItem,
                            AmountInGrams = foodItemBindingModel.AmountInGrams
                        };
                        mealFoodItems.Add(mealFoodItem);
                    }
                }
            }

            if (model.Recipes != null)
            {
                foreach (var recipeBindingModel in model.Recipes)
                {
                    var recipe = this.context.Recipes.FirstOrDefault(r => r.Name == recipeBindingModel.Name);
                    if (recipe != null && recipeBindingModel.AmountInGrams > 0)
                    {
                        var mealRecipe = new MealRecipe
                        {
                            Recipe = recipe,
                            AmountInGrams = recipeBindingModel.AmountInGrams
                        };
                        mealRecipes.Add(mealRecipe);
                    }
                }
            }

            var meal = new Meal
            {
                FoodyUser = user,
                Location = location,
                MealFoodItems = mealFoodItems,
                MealRecipes = mealRecipes,
                Note = model.Note,
                TimeOfConsumption = model.TimeOfConsumption
            };



            this.context.Meals.Add(meal);

            this.context.SaveChanges();

            await this.SetMealCaloriesAsync(meal.Id);

            return meal;
        }

        public MealsListViewModel GetMealsListByPeriod(DateTime startDateTime, DateTime endDateTime, string username)
        {
            var meals = this.context.Meals.Include(m => m.FoodyUser).Include(m => m.Location)
                .Where(m => m.FoodyUser.UserName == username && m.TimeOfConsumption >= startDateTime &&
                            m.TimeOfConsumption <= endDateTime)
                .Select(m => new MealForEditingListViewModel
                {
                    MealId = m.Id,
                    Note = m.Note,
                    TimeOfConsumption = m.TimeOfConsumption,
                    CaloriesFromProteins = m.CaloriesFromProteins,
                    CaloriesFromCarbohydrates = m.CaloriesFromCarbohydrates,
                    CaloriesFromFats = m.CaloriesFromFats,
                    TotalCalories = m.TotalCalories,
                    Location = m.Location.Name
                }).ToList();

            var mealsList = new MealsListViewModel
            {
                Items = meals,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime
            };

            mealsList.PaginationModel.TotalPages = this.paginationService.GetTotalPages(mealsList.Items.Count);

            return mealsList;
        }

        public EditMealViewModel GetMealForEditing(string mealId)
        {
            var meal = this.context.Meals.Include(m => m.Location).Include(m => m.MealFoodItems).ThenInclude(mfi => mfi.FoodItem)
                .Include(m => m.MealRecipes).ThenInclude(mr => mr.Recipe).FirstOrDefault(m => m.Id == mealId);

            if (meal == null)
            {
                return null;
            }

            var recipes = new List<EditMealViewModel.RecipeInEditMealViewModel>();
            var foodItems = new List<EditMealViewModel.FoodItemInEditMealViewModel>();

            foreach (var mealRecipe in meal.MealRecipes)
            {
                var recipeInEditMealViewModel = new EditMealViewModel.RecipeInEditMealViewModel
                {
                    Name = mealRecipe.Recipe.Name,
                    AmountInGrams = mealRecipe.AmountInGrams
                };
                recipes.Add(recipeInEditMealViewModel);
            }

            foreach (var mealFoodItem in meal.MealFoodItems)
            {
                var foodItemInEditMealViewModel = new EditMealViewModel.FoodItemInEditMealViewModel
                {
                    Name = mealFoodItem.FoodItem.Name,
                    AmountInGrams = mealFoodItem.AmountInGrams
                };
                foodItems.Add(foodItemInEditMealViewModel);
            }

            var editMealViewModel = new EditMealViewModel
            {
                Id = meal.Id,
                Note = meal.Note,
                Location = meal.Location == null ? null : meal.Location.Name,
                TimeOfConsumption = meal.TimeOfConsumption,
                Recipes = recipes,
                FoodItems = foodItems
            };

            return editMealViewModel;
        }

        public async Task SetMealCaloriesAsync(string mealId)
        {
            var task = Task.Run(() =>
            {
                var meal = this.context.Meals.FirstOrDefault(m => m.Id == mealId);

                if (meal == null)
                {
                    return;
                }

                var caloriesFromSeparateMacroElementsProperties = meal.GetType().GetProperties()
                    .Where(p => p.Name.Contains("CaloriesFrom")).ToArray();

                foreach (var property in caloriesFromSeparateMacroElementsProperties)
                {
                    this.SetCaloriesFromType(meal.Id, property.Name.Replace("CaloriesFrom", string.Empty));
                }
                this.SetTotalMealCalories(meal.Id);
            });

            await task;
        }

        private void SetCaloriesFromType(string mealId, string macroElementTypeName)
        {

            var meal = this.context.Meals.Include(m => m.MealRecipes).Include(m => m.MealFoodItems).FirstOrDefault(m => m.Id == mealId);

            if (meal == null)
            {
                return;
            }

            var recipesCalories = 0d;

            if (meal.MealRecipes.Count != 0)
            {
                var mealRecipesModel = this.context.Meals
                    .Where(m => m.Id == mealId).Select(m => new
                    {
                        MealRecipes = m.MealRecipes.Select(mr => new
                        {
                            RecipeAmountInMeal = mr.AmountInGrams,
                            RecipeCalories = mr.Recipe.RecipeFoodItems.Select(rfi => new
                            {
                                FoodItemCalories = rfi.FoodItem.FoodItemMacroElements.Where(fime => fime.MacroElement.Name == macroElementTypeName).Select(fime => new
                                {

                                    MacroElementsCalories =
                                        fime.MacroElement.CaloricContentPerGram * fime.AmountInGrams
                                }).Select(x => x.MacroElementsCalories),
                                RecipeAmountIngrams = rfi.AmountInGrams
                            })
                        })
                    }).ToArray();

                recipesCalories = mealRecipesModel.SelectMany(x => x.MealRecipes.Select(y =>
                        (y.RecipeAmountInMeal / y.RecipeCalories.Select(z => z.RecipeAmountIngrams).Sum()) * y.RecipeCalories.Select(rc =>
                            rc.FoodItemCalories.Select(fic => fic * (rc.RecipeAmountIngrams / 100)).Sum()).Sum()))
                    .Sum();
            }

            var foodItemCalories = 0d;
            if (meal.MealFoodItems.Count != 0)
            {
                var mealFoodItemsModel = this.context.Meals
                    .Where(m => m.Id == mealId).Select(m => new
                    {
                        MealFoodItems = m.MealFoodItems.Select(mfi => new
                        {
                            MealFoodItemInGrams = mfi.AmountInGrams,
                            FoodItemsCalories = mfi.FoodItem.FoodItemMacroElements.Where(fime => fime.MacroElement.Name == macroElementTypeName).Select(fime => new
                            {
                                MacroElementsCalories = fime.MacroElement.CaloricContentPerGram * fime.AmountInGrams
                            }).Select(x => x.MacroElementsCalories)
                        })
                    }).ToArray();

                foodItemCalories = mealFoodItemsModel.SelectMany(x =>
                        x.MealFoodItems.Select(y => y.FoodItemsCalories.Select(z => z * (y.MealFoodItemInGrams / 100)).Sum()))
                    .Sum();
            }

            var totalCalories = recipesCalories + foodItemCalories;

            var calorieProperty = meal.GetType().GetProperty($"CaloriesFrom{macroElementTypeName}");
            if (calorieProperty != null)
            {
                calorieProperty.SetValue(meal, totalCalories);
                this.context.SaveChanges();
            }
        }

        private void SetTotalMealCalories(string mealId)
        {

            var meal = this.context.Meals.Include(m => m.MealRecipes).Include(m => m.MealFoodItems).FirstOrDefault(m => m.Id == mealId);

            if (meal == null)
            {
                return;
            }

            var caloriesFromSeparateMacroElementsProperties = meal.GetType().GetProperties()
                .Where(p => p.Name.Contains("CaloriesFrom")).ToArray();

            var totalCalories = 0d;

            foreach (var property in caloriesFromSeparateMacroElementsProperties)
            {
                var value = (double)property.GetValue(meal);
                totalCalories += value;
            }

            meal.TotalCalories = totalCalories;

            this.context.SaveChanges();

        }

        public async Task SetTotalMealCaloriesAsync(string mealId)
        {
            var task = Task.Run(() =>
            {
                var meal = this.context.Meals.Include(m => m.MealRecipes).Include(m => m.MealFoodItems).FirstOrDefault(m => m.Id == mealId);

                if (meal == null)
                {
                    return;
                }

                var recipesCalories = 0d;

                if (meal.MealRecipes.Count != 0)
                {
                    var mealRecipesModel = this.context.Meals
                        .Where(m => m.Id == mealId).Select(m => new
                        {
                            MealRecipes = m.MealRecipes.Select(mr => new
                            {
                                RecipeAmountInMeal = mr.AmountInGrams,
                                RecipeCalories = mr.Recipe.RecipeFoodItems.Select(rfi => new
                                {
                                    FoodItemCalories = rfi.FoodItem.FoodItemMacroElements.Select(fime => new
                                    {

                                        MacroElementsCalories =
                                            fime.MacroElement.CaloricContentPerGram * fime.AmountInGrams
                                    }).Select(x => x.MacroElementsCalories),
                                    RecipeAmountIngrams = rfi.AmountInGrams
                                })
                            })
                        }).ToArray();

                    recipesCalories = mealRecipesModel.SelectMany(x => x.MealRecipes.Select(y =>
                            (y.RecipeAmountInMeal / y.RecipeCalories.Select(z => z.RecipeAmountIngrams).Sum()) * y.RecipeCalories.Select(rc =>
                                rc.FoodItemCalories.Select(fic => fic * (rc.RecipeAmountIngrams / 100)).Sum()).Sum()))
                        .Sum();
                }

                var foodItemCalories = 0d;
                if (meal.MealFoodItems.Count != 0)
                {
                    var mealFoodItemsModel = this.context.Meals
                        .Where(m => m.Id == mealId).Select(m => new
                        {
                            MealFoodItems = m.MealFoodItems.Select(mfi => new
                            {
                                MealFoodItemInGrams = mfi.AmountInGrams,
                                FoodItemsCalories = mfi.FoodItem.FoodItemMacroElements.Select(fime => new
                                {
                                    MacroElementsCalories = fime.MacroElement.CaloricContentPerGram * fime.AmountInGrams
                                }).Select(x => x.MacroElementsCalories)
                            })
                        }).ToArray();

                    foodItemCalories = mealFoodItemsModel.SelectMany(x =>
                            x.MealFoodItems.Select(y => y.FoodItemsCalories.Select(z => z * (y.MealFoodItemInGrams / 100)).Sum()))
                        .Sum();
                }

                var totalCalories = recipesCalories + foodItemCalories;

                meal.TotalCalories = totalCalories;

                this.context.SaveChangesAsync();
            });

            await task;
        }


        private Location HandleLocation(string locationName, string userId)
        {
            var location = this.context.Locations.Include(l => l.FoodyUser)
                .FirstOrDefault(l => l.FoodyUser.Id == userId && l.Name == locationName);

            if (location == null)
            {
                location = new Location
                {
                    FoodyUserId = userId,
                    Name = locationName
                };
            }

            return location;
        }
    }
}
