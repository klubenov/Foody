using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Articles
{
    public class AllMyApprovedArticlesViewModel
    {
        public AllMyApprovedArticlesViewModel()
        {
            this.MyApprovedArticlesListViewModels = new List<MyApprovedArticlesListViewModel>();
        }

        public List<MyApprovedArticlesListViewModel> MyApprovedArticlesListViewModels { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
