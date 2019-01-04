using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foody.Data;
using Foody.Data.Models.Nutrition;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Content;
using Foody.Services.Models.Content;
using Foody.Services.Models.Knowledge;
using Microsoft.EntityFrameworkCore;
using Foody.Services.Models.Shared;

namespace Foody.Services.DataServices.Knowledge
{
    public class KnowledgeService : IKnowledgeService
    {
        private readonly FoodyDbContext context;
        private readonly IPaginationService paginationService;
        private readonly IContentService contentService;

        public KnowledgeService(FoodyDbContext context, IPaginationService paginationService, IContentService contentService)
        {
            this.context = context;
            this.paginationService = paginationService;
            this.contentService = contentService;
        }

        public KnowledgeListViewModel GetItemList(KnowledgeListViewModel model)
        {
            var items = new List<KnowledgeItemListViewModel>();

            if (!string.IsNullOrEmpty(model.PaginationModel.SearchText) && model.PaginationModel.SearchText == Constants.FoodItemName)
            {
                items.AddRange(this.context.FoodItems.Select(fi => new KnowledgeItemListViewModel
                {
                    Id = fi.Id,
                    Name = fi.Name,
                    Type = Constants.FoodItemName
                }));
            }
            else if (!string.IsNullOrEmpty(model.PaginationModel.SearchText) && model.PaginationModel.SearchText == Constants.MicroElementName)
            {
                items.AddRange(this.context.MicroElements.Select(fi => new KnowledgeItemListViewModel
                {
                    Id = fi.Id,
                    Name = fi.Name,
                    Type = Constants.MicroElementName
                }));
            }
            else if (!string.IsNullOrEmpty(model.PaginationModel.SearchText) && model.PaginationModel.SearchText == Constants.MacroElementName)
            {
                items.AddRange(this.context.MacroElements.Select(fi => new KnowledgeItemListViewModel
                {
                    Id = fi.Id,
                    Name = fi.Name,
                    Type = Constants.MacroElementName
                }));
            }
            else if (!string.IsNullOrEmpty(model.PaginationModel.SearchText) && model.PaginationModel.SearchText == Constants.Recipename)
            {
                items.AddRange(this.context.Recipes.Select(fi => new KnowledgeItemListViewModel
                {
                    Id = fi.Id,
                    Name = fi.Name,
                    Type = Constants.Recipename
                }));
            }
            else
            {
                var microElements = this.context.MicroElements.Where(me => me.Name.Contains(model.PaginationModel.SearchText ?? string.Empty)).Select(
                    me => new KnowledgeItemListViewModel
                    {
                        Id = me.Id,
                        Name = me.Name,
                        Type = Constants.MicroElementName
                    }).ToList();

                var macroElements = this.context.MacroElements.Where(me => me.Name.Contains(model.PaginationModel.SearchText ?? string.Empty)).Select(
                    me => new KnowledgeItemListViewModel
                    {
                        Id = me.Id,
                        Name = me.Name,
                        Type = Constants.MacroElementName
                    }).ToList();

                var foodItems = this.context.FoodItems.Where(fi => fi.Name.Contains(model.PaginationModel.SearchText ?? string.Empty)).Select(
                    me => new KnowledgeItemListViewModel
                    {
                        Id = me.Id,
                        Name = me.Name,
                        Type = Constants.FoodItemName
                    }).ToList();

                var recipes = this.context.Recipes.Where(r => r.Name.Contains(model.PaginationModel.SearchText ?? string.Empty)).Select(
                    me => new KnowledgeItemListViewModel
                    {
                        Id = me.Id,
                        Name = me.Name,
                        Type = Constants.Recipename
                    }).ToList();

                items.AddRange(microElements);
                items.AddRange(macroElements);
                items.AddRange(foodItems);
                items.AddRange(recipes);
            }

            var resultListViewModel = new KnowledgeListViewModel
            {
                Items = items
            };

            resultListViewModel.PaginationModel.TotalPages =
                this.paginationService.GetTotalPages(resultListViewModel.Items.Count);

            return resultListViewModel;
        }

        public KnowledgeItemViewModel GetItem(string itemId)
        {
            var macroElement = this.context.MacroElements.FirstOrDefault(me => me.Id == itemId);

            if (macroElement != null)
            {
                var itemViewModel = new KnowledgeItemViewModel
                {
                    Id = macroElement.Id,
                    Name = macroElement.Name,
                    Description = macroElement.Description,
                    ImageLocation = macroElement.ImageLocation,
                    IsMacroElement = true,
                    MacroElementCaloricContent = macroElement.CaloricContentPerGram
                };

                return itemViewModel;
            }

            var microElement = this.context.MicroElements.FirstOrDefault(me => me.Id == itemId);

            if (microElement != null)
            {
                var itemViewModel = new KnowledgeItemViewModel
                {
                    Id = microElement.Id,
                    Name = microElement.Name,
                    Description = microElement.Description,
                    ImageLocation = microElement.ImageLocation,
                    IsMicroElement = true,
                    MicroElementType = microElement.IsVitamin ? "Vitamin" : microElement.IsMineral ? "Mineral" : microElement.IsOther ? "Other" : "Invalid Type"
                };

                return itemViewModel;
            }

            var foodItem = this.context.FoodItems.Include(fi => fi.FoodItemMicroElements).ThenInclude(fime => fime.MicroElement)
                .Include(fi => fi.FoodItemMacroElements).ThenInclude(fime => fime.MacroElement)
                .FirstOrDefault(fi => fi.Id == itemId);

            if (foodItem != null)
            {
                var microElementsInFoodItem = new List<KnowledgeItemViewModel.MicroElementInFoodItemViewModel>();
                var macroElementsInFoodItem = new List<KnowledgeItemViewModel.MacroElementInFoodItemViewModel>();

                foreach (var foodItemMicroElement in foodItem.FoodItemMicroElements)
                {
                    var microElementInFoodItemViewModel = new KnowledgeItemViewModel.MicroElementInFoodItemViewModel
                    {
                        Id = foodItemMicroElement.MicroElement.Id,
                        Name = foodItemMicroElement.MicroElement.Name,
                        Amount = foodItemMicroElement.AmountInMilligrams
                    };
                    microElementsInFoodItem.Add(microElementInFoodItemViewModel);
                }

                foreach (var foodItemMacroElement in foodItem.FoodItemMacroElements)
                {
                    var macroElementInFoodItemViewModel = new KnowledgeItemViewModel.MacroElementInFoodItemViewModel
                    {
                        Id = foodItemMacroElement.MacroElement.Id,
                        Name = foodItemMacroElement.MacroElement.Name,
                        Amount = foodItemMacroElement.AmountInGrams
                    };
                    macroElementsInFoodItem.Add(macroElementInFoodItemViewModel);
                }

                var itemViewModel = new KnowledgeItemViewModel
                {
                    Id = foodItem.Id,
                    Name = foodItem.Name,
                    Description = foodItem.Description,
                    ImageLocation = foodItem.ImageLocation,
                    MicroElementInFoodItemViewModels = microElementsInFoodItem,
                    MacroElementInFoodItemViewModels = macroElementsInFoodItem,
                    IsFoodItem = true
                };

                return itemViewModel;
            }

            var recipe = this.context.Recipes.Include(r => r.RecipeFoodItems).ThenInclude(rfi => rfi.FoodItem)
                .FirstOrDefault(r => r.Id == itemId);

            if (recipe != null)
            {
                var foodItemsInRecipe = new List<KnowledgeItemViewModel.FoodItemInRecipeViewModel>();

                foreach (var recipeFoodItem in recipe.RecipeFoodItems)
                {
                    var foodItemInRecipe = new KnowledgeItemViewModel.FoodItemInRecipeViewModel
                    {
                        Id = recipeFoodItem.FoodItemId,
                        Name = recipeFoodItem.FoodItem.Name,
                        Amount = recipeFoodItem.AmountInGrams
                    };
                    foodItemsInRecipe.Add(foodItemInRecipe);
                }

                var itemViewModel = new KnowledgeItemViewModel
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    Description = recipe.Description,
                    ImageLocation = recipe.ImageLocation,
                    FoodItemInRecipeViewModels = foodItemsInRecipe,
                    IsRecipe = true,
                    RecipeCaloricContentPer100Grams = this.contentService.GetRecipeCaloricContentPer100GramsById(recipe.Id)
                };

                return itemViewModel;
            }

            return null;
        }
    }
}
