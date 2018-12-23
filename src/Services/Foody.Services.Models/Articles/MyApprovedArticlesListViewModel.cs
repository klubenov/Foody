using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Articles
{
    public class MyApprovedArticlesListViewModel
    {
        public string ArticleId { get; set; }

        public string Title { get; set; }

        public DateTime ApprovedOn { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
