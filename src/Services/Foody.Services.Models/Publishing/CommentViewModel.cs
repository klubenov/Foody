using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Publishing
{
    public class CommentViewModel
    {
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
