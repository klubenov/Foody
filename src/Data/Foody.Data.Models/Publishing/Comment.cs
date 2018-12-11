using System;

namespace Foody.Data.Models.Publishing
{
    public class Comment : BaseModel
    {
        public string Content { get; set; }

        public DateTime PostDate { get; set; }

        public string ArticleId { get; set; }
        public Article Article { get; set; }

        public string AuthorId { get; set; }
        public FoodyUser Author { get; set; }
    }
}