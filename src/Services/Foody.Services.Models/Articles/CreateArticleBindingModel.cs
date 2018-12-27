using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using Foody.Services.Models.Shared;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.Models.Articles
{
    public class CreateArticleBindingModel
    {
        [Required(ErrorMessage = Constants.ArticleTitleMissingErrorMessage)]
        [StringLength(Constants.ArticleTitleMaxLength, MinimumLength = Constants.ArticleTitleMinLength, ErrorMessage = Constants.ArticleTitleLengthErrorMessage)]
        public string Title { get; set; }

        [Required(ErrorMessage = Constants.ArticleContentMissingErrorMessage)]
        [StringLength(Constants.ArticleContentMaxLength, MinimumLength = Constants.ArticleContentMinLength, ErrorMessage = Constants.ArticleContentLengthErrorMessage)]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
