using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Content
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
