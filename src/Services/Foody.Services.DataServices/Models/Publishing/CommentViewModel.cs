using System;

namespace Foody.Services.DataServices.Models.Publishing
{
    public class CommentViewModel
    {
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime PostedOn { get; set; }
    }
}
