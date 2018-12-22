using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Articles
{
    public class AllArticlesForApprovalViewModel : IPaginateable<ArticleForApprovalListViewModel>
    {
        public AllArticlesForApprovalViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<ArticleForApprovalListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }

        public string ErrorMessage { get; set; }
    }
}
