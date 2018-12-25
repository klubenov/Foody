using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Articles
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
