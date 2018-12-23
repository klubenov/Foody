using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Articles
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
