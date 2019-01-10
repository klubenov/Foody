using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Content
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
