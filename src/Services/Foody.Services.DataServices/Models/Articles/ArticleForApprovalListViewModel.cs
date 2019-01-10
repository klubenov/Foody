using System;

namespace Foody.Services.DataServices.Models.Articles
{
    public class ArticleForApprovalListViewModel
    {
        public string ArticleId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
