using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
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
                }).OrderBy(m => m.TimeOfConsumption).ToList();

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

        public async Task<Meal> EditMeal(EditMealViewModel model)
        {
            var meal = this.context.Meals.Include(m => m.Location).Include(m => m.FoodyUser).Include(m => m.MealFoodItems).ThenInclude(mfi => mfi.FoodItem)
                .Include(m => m.MealRecipes).ThenInclude(mr => mr.Recipe)
                .FirstOrDefault(m => m.Id == model.Id);

            if (meal == null)
            {
                return null;
            }

            var caloriesChange = false;
            if (model.FoodItems != null)
            {
                foreach (var foodItemViewModel in model.FoodItems)
                {
                    var foodItem = this.context.FoodItems.FirstOrDefault(fi => fi.Name == foodItemViewModel.Name);

                    if (foodItem == null)
                    {
                        continue;
                    }

                    var existingMealFoodItem = meal.MealFoodItems.FirstOrDefault(mfi => mfi.FoodItem.Name == foodItemViewModel.Name);

                    if (foodItemViewModel.AmountInGrams > 0)
                    {
                        if (existingMealFoodItem == null)
                        {
                            var newMealFoodItem = new MealFoodItem
                            {
                                Meal = meal,
                                FoodItem = foodItem,
                                AmountInGrams = foodItemViewModel.AmountInGrams
                            };
                            meal.MealFoodItems.Add(newMealFoodItem);
                            caloriesChange = true;
                        }
                        else
                        {
                            if (!existingMealFoodItem.AmountInGrams.Equals(foodItemViewModel.AmountInGrams))
                            {
                                existingMealFoodItem.AmountInGrams = foodItemViewModel.AmountInGrams;
                                caloriesChange = true;
                            }
                        }
                    }
                    else
                    {
                        if (existingMealFoodItem != null)
                        {
                            meal.MealFoodItems.Remove(existingMealFoodItem);
                            caloriesChange = true;
                        }
                    }
                }
            }

            if (model.Recipes != null)
            {
                foreach (var recipeViewModel in model.Recipes)
                {
                    var recipe = this.context.Recipes.FirstOrDefault(r => r.Name == recipeViewModel.Name);

                    if (recipe == null)
                    {
                        continue;
                    }

                    var existingMealRecipe = meal.MealRecipes.FirstOrDefault(mr => mr.Recipe.Name == recipeViewModel.Name);

                    if (recipeViewModel.AmountInGrams > 0)
                    {
                        if (existingMealRecipe == null)
                        {
                            var newMealRecipe = new MealRecipe
                            {
                                Meal = meal,
                                Recipe = recipe,
                                AmountInGrams = recipeViewModel.AmountInGrams
                            };
                            meal.MealRecipes.Add(newMealRecipe);
                            caloriesChange = true;
                        }
                        else
                        {
                            if (!existingMealRecipe.AmountInGrams.Equals(recipeViewModel.AmountInGrams))
                            {
                                existingMealRecipe.AmountInGrams = recipeViewModel.AmountInGrams;
                                caloriesChange = true;
                            }
                        }
                    }
                    else
                    {
                        if (existingMealRecipe != null)
                        {
                            meal.MealRecipes.Remove(existingMealRecipe);
                            caloriesChange = true;
                        }
                    }
                }
            }

            meal.Note = model.Note;

            if (meal.Location == null || meal.Location.Name != model.Location)
            {
                meal.Location = this.HandleLocation(model.Location, meal.FoodyUserId);
            }

            meal.TimeOfConsumption = model.TimeOfConsumption;

            context.SaveChanges();

            if (caloriesChange)
            {
                Thread.Sleep(500);
                await this.SetMealCaloriesAsync(meal.Id);
            }

            return meal;
        }

        public DiaryIndexViewModel GetIndexModel(string username)
        {
            var indexModel = new DiaryIndexViewModel
            {
                InitialOpen = true
            };

            indexModel.HomePageStatistics = this.GetStatistics(DateTime.Now.AddMonths(-1), DateTime.Now, username);

            return indexModel;
        }

        public StatisticsViewModel GetStatistics(DateTime modelStartCustomDate, DateTime modelEndCustomDate, string username)
        {
            var user = this.context.Users.Include(u => u.Meals)
                .ThenInclude(m => m.MealFoodItems).ThenInclude(mfi => mfi.FoodItem)
                .Include(u => u.Meals)
                .ThenInclude(m => m.MealRecipes).ThenInclude(mr => mr.Recipe).ThenInclude(r => r.RecipeFoodItems)
                .ThenInclude(rfi => rfi.FoodItem)
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }

            var meals = user.Meals
                .Where(m => m.FoodyUser.UserName == username)
                .Where(m => m.TimeOfConsumption >= modelStartCustomDate && m.TimeOfConsumption <= modelEndCustomDate).ToArray();

            var periodDays = (modelEndCustomDate.Date - modelStartCustomDate.Date).Days;
            int days = 0;

            for (int i = 0; i <= periodDays; i++)
            {
                var currentDayTimeInPeriod = modelStartCustomDate.AddDays(i);

                if (meals.Any(m => m.TimeOfConsumption.Date == currentDayTimeInPeriod.Date))
                {
                    days++;
                }
            }

            var statisticsViewModel = new StatisticsViewModel
            {
                AverageDailyCalories = meals.Select(m => m.TotalCalories).Sum() / days
            };

            var caloriesFromFats = meals.Select(m => m.CaloriesFromFats).Sum();
            var caloriesFromCarbs = meals.Select(m => m.CaloriesFromCarbohydrates).Sum();
            var caloriesFromProtein = meals.Select(m => m.CaloriesFromProteins).Sum();
            var totalCalories = meals.Select(m => m.TotalCalories).Sum();

            statisticsViewModel.CaloricRatio = totalCalories.Equals(0) ? "There are no meals for the period.." :
                $"{Math.Round(caloriesFromFats / totalCalories * 100)}% / {Math.Round(caloriesFromCarbs / totalCalories * 100)}% / {Math.Round(caloriesFromProtein / totalCalories * 100)}%";

            var foodItems = meals.SelectMany(m => m.MealFoodItems).Select(mfi => mfi.FoodItem);
            var foodItemsFromRecipes = meals.SelectMany(m => m.MealRecipes).Select(mr => mr.Recipe)
                .SelectMany(r => r.RecipeFoodItems).Select(rfi => rfi.FoodItem);

            var foodItemOccurenceDict = new Dictionary<FoodItem,int>();
            var recipeOccurenceDict = new Dictionary<Recipe, int>();

            foreach (var foodItem in foodItems)
            {
                if (foodItemOccurenceDict.ContainsKey(foodItem))
                {
                    foodItemOccurenceDict[foodItem]++;
                    continue;
                }
                foodItemOccurenceDict.Add(foodItem,1);
            }

            foreach (var foodItem in foodItemsFromRecipes)
            {
                if (foodItemOccurenceDict.ContainsKey(foodItem))
                {
                    foodItemOccurenceDict[foodItem]++;
                    continue;
                }
                foodItemOccurenceDict.Add(foodItem, 1);
            }

            var mostUsedFoodItemKeyValuePair = foodItemOccurenceDict.OrderByDescending(x => x.Value).FirstOrDefault();
            statisticsViewModel.MostUsedFoodItem = mostUsedFoodItemKeyValuePair.Key == null ? "There are no meals for the period.." : mostUsedFoodItemKeyValuePair.Key.Name;
            statisticsViewModel.MostUsedFoodItemId = mostUsedFoodItemKeyValuePair.Key == null ? string.Empty : mostUsedFoodItemKeyValuePair.Key.Id;

            var recipes = meals.SelectMany(m => m.MealRecipes).Select(mr => mr.Recipe);

            foreach (var recipe in recipes)
            {
                if (recipeOccurenceDict.ContainsKey(recipe))
                {
                    recipeOccurenceDict[recipe]++;
                    continue;
                }
                recipeOccurenceDict.Add(recipe, 1);
            }

            var mostUsedRecipeKeyValuePair = recipeOccurenceDict.OrderByDescending(x => x.Value).FirstOrDefault();
            statisticsViewModel.MostUsedRecipe = mostUsedRecipeKeyValuePair.Key == null ? "There are no meals for the period.." : mostUsedRecipeKeyValuePair.Key.Name;
            statisticsViewModel.MostUsedRecipeId = mostUsedRecipeKeyValuePair.Key == null ? string.Empty : mostUsedRecipeKeyValuePair.Key.Id;

            return statisticsViewModel;
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
