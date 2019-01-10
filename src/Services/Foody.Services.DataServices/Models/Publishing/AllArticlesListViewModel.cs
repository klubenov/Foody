using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Publishing
{
    public class AllArticlesListViewModel : IPaginateable<ArticleListViewModel>
    {
        public AllArticlesListViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<ArticleListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}
