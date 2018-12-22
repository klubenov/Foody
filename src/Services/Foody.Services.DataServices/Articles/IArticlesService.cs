using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Articles;

namespace Foody.Services.DataServices.Articles
{
    public interface IArticlesService
    {
        void CreateArticle(CreateArticleBindingModel createArticleBindingModel, string authorName);

        AllArticlesForApprovalViewModel GetAllArticlesForApproval();

        ArticleForApprovalViewModel GetArticleForApproval(string id);

        bool ApproveArticle(string articleId);

        bool RejectArticle(string articleId, string rejectComment);
    }
}
