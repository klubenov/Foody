using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Publishing
{
    public class ArticleListViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
