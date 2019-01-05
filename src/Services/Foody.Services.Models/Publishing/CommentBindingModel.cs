using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foody.Services.Models.Publishing
{
    public class CommentBindingModel
    {
        public string ArticleId { get; set; }

        [StringLength(1000, MinimumLength = 50, ErrorMessage = "Comment too long or too short, should be between 50 and 1000 characters")]
        [Required(ErrorMessage = "Please enter a comment..")]
        public string Content { get; set; }
    }
}
