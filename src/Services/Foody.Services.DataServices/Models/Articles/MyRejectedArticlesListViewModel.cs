using System;

namespace Foody.Services.DataServices.Models.Articles
{
    public class MyRejectedArticlesListViewModel
    {
        public string ArticleId { get; set; }

        public string Title { get; set; }

        public DateTime LastRejectOn { get; set; }

        public DateTime PostedOn { get; set; }

        public string RejectedBy { get; set; }
    }
}
