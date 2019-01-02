using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Content
{
    public class AllEditRecipesViewModel : IPaginateable<EditRecipeListViewModel>
    {
        public AllEditRecipesViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<EditRecipeListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }

        public string SearchText { get; set; }
    }
}
