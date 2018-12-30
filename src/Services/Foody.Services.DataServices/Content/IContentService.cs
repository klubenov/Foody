using System;
using System.Collections.Generic;
using System.Text;
using Foody.Data.Models.Nutrition;
using Foody.Services.Models.Content;

namespace Foody.Services.DataServices.Content
{
    public interface IContentService
    {
        MicroElement AddMicroElement(AddMicroElementBindingModel addMicroElementBindingModel);

        AllEditMicroElementsViewModel GetAllMicroElementsForEditing(string searchText);

        EditMicroElementViewModel GetMicroElementForEditing(string microElementId);

        MicroElement EditMicroElement(EditMicroElementViewModel model);

        MacroElement AddMacroElement(AddMacroElementBindingModel addMacroElementBindingModel);

        IEnumerable<string> GetMicroElementsNames();

        IEnumerable<string> GetMacroElementsNames();

        IEnumerable<string> GetFoodItemsNames();

        FoodItem AddFoodItem(AddFoodItemBindingModel model);

        AllEditFoodItemsViewModel GetAllEditFoodItemsForEditing(string searchText);

        EditFoodItemViewModel GetFoodItemForEditing(string foodItemId);

        FoodItem EditFoodItem(EditFoodItemViewModel model);
    }
}
