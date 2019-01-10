using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Content
{
    public class AllEditFoodItemsViewModel : IPaginateable<EditFoodItemListViewModel>
    {
        public AllEditFoodItemsViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<EditFoodItemListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }

        public string SearchText { get; set; }
    }
}
