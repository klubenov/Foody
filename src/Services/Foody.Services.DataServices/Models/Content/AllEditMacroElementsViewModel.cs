using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Content
{
    public class AllEditMacroElementsViewModel : IPaginateable<EditMacroElementListViewModel>
    {
        public AllEditMacroElementsViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<EditMacroElementListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }

        public string SearchText { get; set; }
    }
}
