namespace Foody.Services.DataServices.Models.Content
{
    public class EditFoodItemListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int UsageInRecipesCount { get; set; }

        public long UsageInMealsCount { get; set; }
    }
}
