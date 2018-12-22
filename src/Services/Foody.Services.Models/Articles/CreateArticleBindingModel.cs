using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Articles
{
    public class CreateArticleBindingModel
    {
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }

        [Required]
        [StringLength(5000, MinimumLength = 50)]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
