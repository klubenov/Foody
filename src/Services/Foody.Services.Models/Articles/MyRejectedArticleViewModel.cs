using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Articles
{
    public class MyRejectedArticleViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string Content { get; set; }

        [Required]
        public DateTime PostedOn { get; set; }

        public DateTime? RejectedOn { get; set; }

        [Required]
        public string ImageLocation { get; set; }

        [Required]
        public int CurrentPage { get; set; }

        [Required]
        public string RejectComment { get; set; }

        public IFormFile NewImage { get; set; }
    }
}
