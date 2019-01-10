using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Articles
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
