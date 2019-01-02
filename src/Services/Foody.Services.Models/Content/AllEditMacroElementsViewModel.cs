using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Content
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
