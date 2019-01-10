using System;
using System.ComponentModel.DataAnnotations;
using Foody.Services.DataServices.Models.Shared;
using Foody.Services.DataServices.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.DataServices.Models.Articles
{
    public class MyRejectedArticleViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required(ErrorMessage = Constants.ArticleTitleMissingErrorMessage)]
        [StringLength(Constants.ArticleTitleMaxLength, MinimumLength = Constants.ArticleTitleMinLength, ErrorMessage = Constants.ArticleTitleLengthErrorMessage)]
        [UniqueArticleTitle]
        public string Title { get; set; }

        [Required(ErrorMessage = Constants.ArticleContentMissingErrorMessage)]
        [StringLength(Constants.ArticleContentMaxLength, MinimumLength = Constants.ArticleContentMinLength, ErrorMessage = Constants.ArticleContentLengthErrorMessage)]
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
