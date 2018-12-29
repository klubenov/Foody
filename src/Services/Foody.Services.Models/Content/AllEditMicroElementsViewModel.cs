using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Content
{
    public class AllEditMicroElementsViewModel : IPaginateable<EditMicroElementListViewModel>
    {
        public AllEditMicroElementsViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<EditMicroElementListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }

        public string SearchText { get; set; }
    }
}
