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

        IEnumerable<string> GetMicroElementsNames();

        IEnumerable<string> GetMacroElementsNames();

        FoodItem AddFoodItem(AddFoodItemBindingModel model);
    }
}
