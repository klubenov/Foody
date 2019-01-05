using System;
using System.Collections.Generic;
using System.Text;
using Foody.Data.Models.Publishing;
using Foody.Services.Models.Publishing;

namespace Foody.Services.DataServices.Publishing
{
    public interface IPublishingService
    {
        AllArticlesListViewModel GetAllArticles();

        ArticleViewModel GetArticle(string articleId);

        Comment SaveComment(CommentBindingModel modelCommentBindingModel, string username);
    }
}
