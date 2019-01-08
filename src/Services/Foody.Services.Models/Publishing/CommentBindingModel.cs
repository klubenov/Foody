using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foody.Services.Models.Publishing
{
    public class CommentBindingModel
    {
        public string ArticleId { get; set; }

        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Comment too long or too short, should be between 20 and 1000 characters")]
        [Required(ErrorMessage = "Please enter a comment..")]
        public string Content { get; set; }
    }
}
