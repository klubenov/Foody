using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Articles
{
    public class AllMyRejectedArticlesViewModel : IPaginateable<MyRejectedArticlesListViewModel>
    {
        public AllMyRejectedArticlesViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<MyRejectedArticlesListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}
