using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Articles
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
