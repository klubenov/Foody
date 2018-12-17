using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Articles
{
    public class AllArticlesForApprovalViewModel
    {
        public AllArticlesForApprovalViewModel()
        {
            this.ArticleForApprovalListViewModels = new List<ArticleForApprovalListViewModel>();
        }

        public List<ArticleForApprovalListViewModel> ArticleForApprovalListViewModels { get; set; }
    }
}
