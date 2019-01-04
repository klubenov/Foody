using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Publishing
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
