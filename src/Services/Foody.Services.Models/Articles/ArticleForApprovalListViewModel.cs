using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Foody.Services.Models.Articles
{
    public class ArticleForApprovalListViewModel
    {
        public string ArticleId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
