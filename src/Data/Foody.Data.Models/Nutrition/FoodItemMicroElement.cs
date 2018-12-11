namespace Foody.Data.Models.Nutrition
{
    public class FoodItemMicroElement
    {
        public string FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }

        public string MicroElementId { get; set; }
        public MicroElement MicroElement { get; set; }

        public double AmountInMilligrams { get; set; }
    }
}
