using System.ComponentModel.DataAnnotations;
using Foody.Services.DataServices.Models.Shared;
using Foody.Services.DataServices.ValidationAttributes;
using Microsoft.AspNetCore.Http;

namespace Foody.Services.DataServices.Models.Articles
{
    public class CreateArticleBindingModel
    {
        [Required(ErrorMessage = Constants.ArticleTitleMissingErrorMessage)]
        [StringLength(Constants.ArticleTitleMaxLength, MinimumLength = Constants.ArticleTitleMinLength, ErrorMessage = Constants.ArticleTitleLengthErrorMessage)]
        [UniqueArticleTitle]
        public string Title { get; set; }

        [Required(ErrorMessage = Constants.ArticleContentMissingErrorMessage)]
        [StringLength(Constants.ArticleContentMaxLength, MinimumLength = Constants.ArticleContentMinLength, ErrorMessage = Constants.ArticleContentLengthErrorMessage)]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
