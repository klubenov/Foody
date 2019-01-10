using System;

namespace Foody.Services.DataServices.Models.Articles
{
    public class ArticleForApprovalViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public string Author { get; set; }

        public string ImageLocation { get; set; }

        public int CurrentPage { get; set; }
    }
}
