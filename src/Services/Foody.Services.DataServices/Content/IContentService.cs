﻿using System;
using System.Collections.Generic;
using System.Text;
using Foody.Data.Models.Nutrition;
using Foody.Services.DataServices.Models.Content;

namespace Foody.Services.DataServices.Content
{
    public interface IContentService
    {
        MicroElement AddMicroElement(AddMicroElementBindingModel addMicroElementBindingModel);

        AllEditMicroElementsViewModel GetAllMicroElementsForEditing(string searchText);

        EditMicroElementViewModel GetMicroElementForEditing(string microElementId);

        MicroElement EditMicroElement(EditMicroElementViewModel model);

        MacroElement AddMacroElement(AddMacroElementBindingModel addMacroElementBindingModel);

        AllEditMacroElementsViewModel GetAllMacroElementsForEditing(string searchText);

        EditMacroElementViewModel GetMacroElementForEditing(string macroElementId);

        MacroElement EditMacroElement(EditMacroElementViewModel model);

        IEnumerable<string> GetMicroElementsNames();

        IEnumerable<string> GetMacroElementsNames();

        IEnumerable<string> GetFoodItemsNames();

        IEnumerable<string> GetRecipesNames();

        FoodItem AddFoodItem(AddFoodItemBindingModel model);

        AllEditFoodItemsViewModel GetAllEditFoodItemsForEditing(string searchText);

        EditFoodItemViewModel GetFoodItemForEditing(string foodItemId);

        FoodItem EditFoodItem(EditFoodItemViewModel model);

        Recipe AddRecipe(AddRecipeBindingModel model);

        AllEditRecipesViewModel GetAllRecipesForEditing(string searchText);

        EditRecipeViewModel GetRecipeForEditing(string recipeId);

        Recipe EditRecipe(EditRecipeViewModel model);

        double GetRecipeCaloricContentPer100GramsById(string recipeId);
    }
}
