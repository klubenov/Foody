using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foody.Data;
using Foody.Data.Models.Nutrition;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Images;
using Foody.Services.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace Foody.Services.DataServices.Content
{
    public class ContentService : IContentService
    {
        private readonly FoodyDbContext context;
        private readonly IImagesService imagesService;
        private readonly IPaginationService paginationService;

        public ContentService(FoodyDbContext context, IImagesService imagesService, IPaginationService paginationService)
        {
            this.context = context;
            this.imagesService = imagesService;
            this.paginationService = paginationService;
        }

        public MicroElement AddMicroElement(AddMicroElementBindingModel model)
        {
            var newMicroElement = new MicroElement
            {
                Name = model.Name,
                Description = model.Description,
                
            };

            switch (model.Type)
            {
                case "Vitamin":
                    newMicroElement.IsVitamin = true;
                    break;
                case "Mineral":
                    newMicroElement.IsMineral = true;
                    break;
                case "Other":
                    newMicroElement.IsOther = true;
                    break;
            }

            this.context.MicroElements.Add(newMicroElement);
            this.context.SaveChanges();

            if (model.Image != null)
            {
                var microElementId = this.context.MicroElements.First(mi => mi.Name == model.Name).Id;

                var imageLocation = this.imagesService.CreateImage(model.Image, this.GetType().Name.Replace("Service", string.Empty), microElementId);

                this.context.MicroElements.First(mi => mi.Id == microElementId).ImageLocation = imageLocation;
                newMicroElement.ImageLocation = imageLocation;
                context.SaveChanges();
            }

            return newMicroElement;
        }

        public AllEditMicroElementsViewModel GetAllMicroElementsForEditing(string searchText)
        {
            var microElements = this.context.MicroElements.Where(me => me.Name.Contains(searchText ?? string.Empty)).OrderByDescending(me => me.IsVitamin).ThenByDescending(me => me.IsMineral).Select(me => new EditMicroElementListViewModel
            {
                Id = me.Id,
                Name = me.Name,
                Type = me.IsVitamin ? "Vitamin" : me.IsMineral ? "Mineral" : me.IsOther ? "Other" : "Invalid Type"
            }).ToList();

            var allMicroElements = new AllEditMicroElementsViewModel
            {
                Items = microElements
            };

            allMicroElements.PaginationModel.TotalPages =
                this.paginationService.GetTotalPages(allMicroElements.Items.Count);

            return allMicroElements;
        }

        public EditMicroElementViewModel GetMicroElementForEditing(string microElementId)
        {
            var microElement = this.context.MicroElements.Where(me => me.Id == microElementId).Select(me => new EditMicroElementViewModel
            {
                Id = me.Id,
                Name = me.Name,
                Description = me.Description,
                ImageLocation = me.ImageLocation,
                Type = me.IsVitamin ? "Vitamin" : me.IsMineral ? "Mineral" : me.IsOther ? "Other" : "Invalid Type"
            }).FirstOrDefault();

            return microElement;
        }

        public MicroElement EditMicroElement(EditMicroElementViewModel model)
        {
            var microElement = this.context.MicroElements.FirstOrDefault(me => me.Id == model.Id);

            if (microElement == null)
            {
                return null;
            }

            microElement.Name = model.Name;
            microElement.Description = model.Description;

            switch (model.Type)
            {
                case "Vitamin":
                    microElement.IsVitamin = true;
                    microElement.IsMineral = false;
                    microElement.IsOther = false;
                    break;
                case "Mineral":
                    microElement.IsVitamin = false;
                    microElement.IsMineral = true;
                    microElement.IsOther = false;
                    break;
                case "Other":
                    microElement.IsVitamin = false;
                    microElement.IsMineral = false;
                    microElement.IsOther = true;
                    break;
            }

            if (model.NewImage != null)
            {
                string newImageLocation = this.imagesService.RecreateImage(model.NewImage, this.GetType().Name.Replace("Service", string.Empty), model.ImageLocation, model.Id);
                microElement.ImageLocation = newImageLocation;
            }

            context.SaveChanges();
            return microElement;
        }

        public MacroElement AddMacroElement(AddMacroElementBindingModel model)
        {
            var newMacroElement = new MacroElement
            {
                Name = model.Name,
                Description = model.Description,
                CaloricContentPerGram = model.CaloricContentPerGram
            };

            this.context.MacroElements.Add(newMacroElement);
            context.SaveChanges();

            if (model.Image != null)
            {
                var macroElementId = this.context.MacroElements.First(mi => mi.Name == model.Name).Id;

                var imageLocation = this.imagesService.CreateImage(model.Image, this.GetType().Name.Replace("Service", string.Empty), macroElementId);

                this.context.MacroElements.First(mi => mi.Id == macroElementId).ImageLocation = imageLocation;
                newMacroElement.ImageLocation = imageLocation;
                context.SaveChanges();
            }

            return newMacroElement;
        }

        public AllEditMacroElementsViewModel GetAllMacroElementsForEditing(string searchText)
        {
            var macroElements = this.context.MacroElements.Where(me => me.Name.Contains(searchText ?? string.Empty))
                .Select(me => new EditMacroElementListViewModel
                {
                    Id = me.Id,
                    Name = me.Name,
                    CaloricContentPerGram = me.CaloricContentPerGram
                }).ToList();

            var allMacroElements = new AllEditMacroElementsViewModel
            {
                Items = macroElements
            };

            allMacroElements.PaginationModel.TotalPages =
                this.paginationService.GetTotalPages(allMacroElements.Items.Count);

            return allMacroElements;
        }

        public EditMacroElementViewModel GetMacroElementForEditing(string macroElementId)
        {
            var macroElement = this.context.MacroElements.Where(me => me.Id == macroElementId).Select(me => new EditMacroElementViewModel
            {
                Id = me.Id,
                Name = me.Name,
                Description = me.Description,
                CaloricContentPerGram = me.CaloricContentPerGram,
                ImageLocation = me.ImageLocation
            }).FirstOrDefault();

            return macroElement;
        }

        public MacroElement EditMacroElement(EditMacroElementViewModel model)
        {
            var macroElement = this.context.MacroElements.FirstOrDefault(me => me.Id == model.Id);

            if (macroElement == null)
            {
                return null;
            }

            macroElement.Name = model.Name;
            macroElement.Description = model.Description;
            macroElement.CaloricContentPerGram = model.CaloricContentPerGram;

            if (model.NewImage != null)
            {
                string newImageLocation = this.imagesService.RecreateImage(model.NewImage, this.GetType().Name.Replace("Service", string.Empty), model.ImageLocation, model.Id);
                macroElement.ImageLocation = newImageLocation;
            }

            context.SaveChanges();
            return macroElement;
        }

        public IEnumerable<string> GetMicroElementsNames()
        {
            var microElements = this.context.MicroElements.OrderByDescending(e => e.IsVitamin).ThenBy(e => e.IsMineral).ThenBy(e => e.Name).Select(me => me.Name).ToArray();

            return microElements;
        }

        public IEnumerable<string> GetMacroElementsNames()
        {
            var macroElements = this.context.MacroElements.Select(me => me.Name).ToArray();

            return macroElements;
        }

        public IEnumerable<string> GetFoodItemsNames()
        {
            var foodItems = this.context.FoodItems.Select(fi => fi.Name).ToArray();

            return foodItems;
        }

        public FoodItem AddFoodItem(AddFoodItemBindingModel model)
        {
            var foodItemMacroElements = new List<FoodItemMacroElement>();
            var foodItemMicroElements = new List<FoodItemMicroElement>();

            foreach (var macroElement in model.MacroElements)
            {
                var foodItemMacroElement = new FoodItemMacroElement
                {
                    AmountInGrams = macroElement.AmountInGramsPer100Grams,
                    MacroElement = this.context.MacroElements.FirstOrDefault(me => me.Name == macroElement.Name)
                };

                foodItemMacroElements.Add(foodItemMacroElement);
            }

            foreach (var microElement in model.MicroElements.Where(mi => mi.AmountInMilligramsPer100Grams > 0))
            {
                var foodItemMicroElement = new FoodItemMicroElement
                {
                    AmountInMilligrams = microElement.AmountInMilligramsPer100Grams,
                    MicroElement = this.context.MicroElements.FirstOrDefault(me => me.Name == microElement.Name)
                };

                foodItemMicroElements.Add(foodItemMicroElement);
            }

            var foodItem = new FoodItem
            {
                Name = model.Name,
                Description = model.Description,
                FoodItemMacroElements = foodItemMacroElements,
                FoodItemMicroElements = foodItemMicroElements
            };

            this.context.FoodItems.Add(foodItem);
            this.context.SaveChanges();

            if (model.Image != null)
            {
                var foodItemId = this.context.FoodItems.First(fi => fi.Name == model.Name).Id;

                var imageLocation = this.imagesService.CreateImage(model.Image, this.GetType().Name.Replace("Service", string.Empty), foodItemId);

                this.context.FoodItems.First(fi => fi.Id == foodItemId).ImageLocation = imageLocation;
                foodItem.ImageLocation = imageLocation;
                context.SaveChanges();
            }

            return foodItem;
        }

        public AllEditFoodItemsViewModel GetAllEditFoodItemsForEditing(string searchText)
        {
            var foodItems = this.context.FoodItems.Include(fi => fi.RecipeFoodItems).Include(fi => fi.MealFoodItems)
                .Where(fi => fi.Name.Contains(searchText ?? string.Empty)).Select(fi => new EditFoodItemListViewModel
                {
                    Id = fi.Id,
                    Name = fi.Name,
                    UsageInRecipesCount = fi.RecipeFoodItems.Count,
                    UsageInMealsCount = this.context.Meals.Include(m => m.MealRecipes)
                                            .ThenInclude(mr => mr.Recipe.RecipeFoodItems)
                                            .ThenInclude(rfi => rfi.FoodItem).Select(m =>
                                                m.MealRecipes.Select(mr =>
                                                    mr.Recipe.RecipeFoodItems.Select(
                                                        rfi => rfi.FoodItem.Name == fi.Name))).Count()
                                        + this.context.Meals.Include(m => m.MealFoodItems).Count()
                }).OrderByDescending(fi => fi.UsageInMealsCount + fi.UsageInRecipesCount).ToList();

            var allFoodItems = new AllEditFoodItemsViewModel
            {
                Items = foodItems
            };

            allFoodItems.PaginationModel.TotalPages = this.paginationService.GetTotalPages(allFoodItems.Items.Count);

            return allFoodItems;
        }

        public EditFoodItemViewModel GetFoodItemForEditing(string foodItemId)
        {
            var foodItem = this.context.FoodItems.Include(fi => fi.FoodItemMicroElements).ThenInclude(fime => fime.MicroElement)
                                                 .Include(fi => fi.FoodItemMacroElements).ThenInclude(fime => fime.MacroElement)
                                                 .FirstOrDefault(fi => fi.Id == foodItemId);

            if (foodItem == null)
            {
                return null;
            }

            var macroElements = new List<EditFoodItemViewModel.MacroElementViewModel>();
            var microElements = new List<EditFoodItemViewModel.MicroElementViewModel>();

            var macroElementsNames = this.GetMacroElementsNames();
            var microElementsNames = this.GetMicroElementsNames();

            foreach (var microElementName in microElementsNames)
            {
                var microElementModel = new EditFoodItemViewModel.MicroElementViewModel
                {
                    Name = microElementName
                };

                if (foodItem.FoodItemMicroElements.Any(fime => fime.MicroElement.Name == microElementName))
                {
                    microElementModel.AmountInMilligramsPer100Grams = foodItem.FoodItemMicroElements
                        .Single(fime => fime.MicroElement.Name == microElementName).AmountInMilligrams;
                }

                microElements.Add(microElementModel);
            }

            foreach (var macroElementName in macroElementsNames)
            {
                var macroElementModel = new EditFoodItemViewModel.MacroElementViewModel
                {
                    Name = macroElementName
                };

                if (foodItem.FoodItemMacroElements.Any(fime => fime.MacroElement.Name == macroElementName))
                {
                    macroElementModel.AmountInGramsPer100Grams = foodItem.FoodItemMacroElements
                        .Single(fime => fime.MacroElement.Name == macroElementName).AmountInGrams;
                }

                macroElements.Add(macroElementModel);
            }

            var foodItemEditModel = new EditFoodItemViewModel
            {
                Id = foodItem.Id,
                Name = foodItem.Name,
                Description = foodItem.Description,
                MacroElements = macroElements,
                MicroElements = microElements,
                ImageLocation = foodItem.ImageLocation
            };

            return foodItemEditModel;
        }

        public FoodItem EditFoodItem(EditFoodItemViewModel model)
        {
            var foodItem = this.context.FoodItems.Include(fi => fi.FoodItemMicroElements).ThenInclude(fime => fime.MicroElement)
                .Include(fi => fi.FoodItemMacroElements).ThenInclude(fime => fime.MacroElement)
                .FirstOrDefault(fi => fi.Id == model.Id);

            if (foodItem == null)
            {
                return null;
            }

            foreach (var macroElementViewModel in model.MacroElements)
            {
                var macroElementInFoodItem = foodItem.FoodItemMacroElements.FirstOrDefault(fime => 
                    fime.MacroElement.Name == macroElementViewModel.Name);
                if (macroElementInFoodItem != null)
                {
                    macroElementInFoodItem.AmountInGrams = macroElementViewModel.AmountInGramsPer100Grams;
                }
                else
                {
                    var newFoodItemMacroElement = new FoodItemMacroElement
                    {
                        FoodItem = foodItem,
                        MacroElement = this.context.MacroElements.First(me => me.Name == macroElementViewModel.Name),
                        AmountInGrams = macroElementViewModel.AmountInGramsPer100Grams
                    };

                    foodItem.FoodItemMacroElements.Add(newFoodItemMacroElement);
                }
            }

            foreach (var microElementViewModel in model.MicroElements)
            {
                var microElementInFoodItem = foodItem.FoodItemMicroElements.FirstOrDefault(fime =>
                        fime.MicroElement.Name == microElementViewModel.Name);
                if (microElementInFoodItem != null)
                {
                    microElementInFoodItem.AmountInMilligrams = microElementViewModel.AmountInMilligramsPer100Grams;
                }
                else
                {
                    var newFoodItemMicroElement = new FoodItemMicroElement
                    {
                        FoodItem = foodItem,
                        MicroElement = this.context.MicroElements.First(me => me.Name == microElementViewModel.Name),
                        AmountInMilligrams = microElementViewModel.AmountInMilligramsPer100Grams
                    };

                    foodItem.FoodItemMicroElements.Add(newFoodItemMicroElement);
                }
            }

            foodItem.Name = model.Name;
            foodItem.Description = model.Description;

            if (model.NewImage != null)
            {
                string newImageLocation = this.imagesService.RecreateImage(model.NewImage, this.GetType().Name.Replace("Service", string.Empty), model.ImageLocation, model.Id);
                foodItem.ImageLocation = newImageLocation;
            }

            context.SaveChanges();
            return foodItem;
        }

        public Recipe AddRecipe(AddRecipeBindingModel model)
        {
            var recipe = new Recipe
            {
                Name = model.Name,
                Description = model.Description
            };

            var recipeFoodItems = model.FoodItems.Select(fi => new RecipeFoodItem
            {
                Recipe = recipe,
                FoodItem = this.context.FoodItems.FirstOrDefault(fidb => fidb.Name == fi.Name),
                AmountInGrams = fi.AmountInGrams
            }).ToList();

            recipe.RecipeFoodItems = recipeFoodItems;
            this.context.Recipes.Add(recipe);
            this.context.SaveChanges();

            if (model.Image != null)
            {
                var recipeId = this.context.Recipes.First(r => r.Name == model.Name).Id;

                var imageLocation = this.imagesService.CreateImage(model.Image, this.GetType().Name.Replace("Service", string.Empty), recipeId);

                this.context.Recipes.First(r => r.Id == recipeId).ImageLocation = imageLocation;
                recipe.ImageLocation = imageLocation;
                context.SaveChanges();
            }

            return recipe;
        }

        public AllEditRecipesViewModel GetAllRecipesForEditing(string searchText)
        {
            var recipes = this.context.Recipes.Include(r => r.RecipeFoodItems).ThenInclude(rfi => rfi.FoodItem)
                .ThenInclude(fi => fi.FoodItemMacroElements).ThenInclude(fime => fime.MacroElement)
                .Where(r => r.Name.Contains(searchText ?? string.Empty))
                .Select(r => new EditRecipeListViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CaloriesPer100grams = r.RecipeFoodItems.SelectMany(rfi =>
                            rfi.FoodItem.FoodItemMacroElements.Select(fime =>
                                fime.MacroElement.CaloricContentPerGram * fime.AmountInGrams *
                                (rfi.AmountInGrams / 100))).Sum() / (r.RecipeFoodItems.Select(rfi => rfi.AmountInGrams).Sum() / 100),
                    IngredientsCount = r.RecipeFoodItems.Count
                }).ToList();

            var allRecipes = new AllEditRecipesViewModel
            {
                Items = recipes
            };

            allRecipes.PaginationModel.TotalPages = this.paginationService.GetTotalPages(allRecipes.Items.Count);

            return allRecipes;
        }

        public EditRecipeViewModel GetRecipeForEditing(string recipeId)
        {
            var recipe = this.context.Recipes.Include(r => r.RecipeFoodItems).ThenInclude(rfi => rfi.FoodItem).Where(r => r.Id == recipeId)
                .Select(r => new EditRecipeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    ImageLocation = r.ImageLocation,
                    FoodItems = r.RecipeFoodItems.Select(rfi => new EditRecipeViewModel.FoodItemInRecipeViewModel
                    {
                        Name = rfi.FoodItem.Name,
                        AmountInGrams = rfi.AmountInGrams
                    }).ToList()
                }).FirstOrDefault();

            return recipe;
        }

        public Recipe EditRecipe(EditRecipeViewModel model)
        {
            var recipe = this.context.Recipes.Include(r => r.RecipeFoodItems).ThenInclude(rfi => rfi.FoodItem).FirstOrDefault(r => r.Id == model.Id);

            if (recipe == null)
            {
                return null;
            }

            foreach (var foodItem in model.FoodItems)
            {
                var recipeFoodItem = recipe.RecipeFoodItems.FirstOrDefault(rfi => rfi.FoodItem.Name == foodItem.Name);

                if (recipeFoodItem != null)
                {
                    if (foodItem.AmountInGrams > 0)
                    {
                        recipeFoodItem.AmountInGrams = foodItem.AmountInGrams;
                    }
                    else
                    {
                        this.context.RecipeFoodItems.Remove(recipeFoodItem);
                    }
                }
                else
                {
                    if (foodItem.AmountInGrams > 0)
                    {
                        var newRecipeFoodItem = new RecipeFoodItem
                        {
                            Recipe = recipe,
                            FoodItem = this.context.FoodItems.First(fi => fi.Name == foodItem.Name),
                            AmountInGrams = foodItem.AmountInGrams
                        };
                        if (newRecipeFoodItem.FoodItem != null)
                        {
                            recipe.RecipeFoodItems.Add(newRecipeFoodItem);
                        }
                    }
                }
            }

            recipe.Name = model.Name;
            recipe.Description = model.Description;

            if (model.NewImage != null)
            {
                string newImageLocation = this.imagesService.RecreateImage(model.NewImage, this.GetType().Name.Replace("Service", string.Empty), model.ImageLocation, model.Id);
                recipe.ImageLocation = newImageLocation;
            }

            context.SaveChanges();
            return recipe;
        }

        public double GetRecipeCaloricContentPer100GramsById(string recipeId)
        {
            var caloricContent = this.context.Recipes.Include(r => r.RecipeFoodItems).ThenInclude(rfi => rfi.FoodItem)
                .ThenInclude(fi => fi.FoodItemMacroElements).ThenInclude(fime => fime.MacroElement)
                .Where(r => r.Id == recipeId)
                .Select(r =>
                    r.RecipeFoodItems.SelectMany(rfi =>
                  rfi.FoodItem.FoodItemMacroElements.Select(fime =>
                 fime.MacroElement.CaloricContentPerGram * fime.AmountInGrams * (rfi.AmountInGrams / 100))).Sum() 
                 / (r.RecipeFoodItems.Select(rfi => rfi.AmountInGrams).Sum() / 100)).FirstOrDefault();

            return caloricContent;
        }
    }
}
