using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Articles
{
    public class MyArticleViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime? ApprovedOn { get; set; }

        public string ImageLocation { get; set; }

        public int CurrentPage { get; set; }

        public string SourceMethodName { get; set; }
    }
}
