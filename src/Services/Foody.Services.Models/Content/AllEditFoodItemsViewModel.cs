using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Content
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
