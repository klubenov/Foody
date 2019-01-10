using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Articles
{
    public class AllMyApprovedArticlesViewModel : IPaginateable<MyApprovedArticlesListViewModel>
    {
        public AllMyApprovedArticlesViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<MyApprovedArticlesListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}
