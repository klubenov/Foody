using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foody.Data;
using Foody.Data.Models.Nutrition;
using Foody.Services.DataServices.Images;
using Foody.Services.Models.Content;

namespace Foody.Services.DataServices.Content
{
    public class ContentService : IContentService
    {
        private readonly FoodyDbContext context;
        private readonly IImagesService imagesService;

        public ContentService(FoodyDbContext context, IImagesService imagesService)
        {
            this.context = context;
            this.imagesService = imagesService;
        }

        public MicroElement AddMicroElement(AddMicroElementBindingModel model)
        {
            var newMicroElement = new MicroElement
            {
                Name = model.Name,
                Description = model.Description,
            };

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
    }
}
