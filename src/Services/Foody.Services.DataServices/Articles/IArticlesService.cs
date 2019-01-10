using System;
using System.Collections.Generic;
using System.Text;
using Foody.Data.Models.Publishing;
using Foody.Services.DataServices.Models.Articles;

namespace Foody.Services.DataServices.Articles
{
    public interface IArticlesService
    {
        Article CreateArticle(CreateArticleBindingModel createArticleBindingModel, string authorName);

        AllArticlesForApprovalViewModel GetAllArticlesForApproval();

        ArticleForApprovalViewModel GetArticleForApproval(string id);

        bool ApproveArticle(string articleId);

        bool RejectArticle(string articleId, string rejectComment, string rejectedBy);

        AllMyApprovedArticlesViewModel GetAllApprovedArticlesByUsername(string username);

        MyArticleViewModel GetMyArticleById(string articleId, string username);

        AllMyRejectedArticlesViewModel GetAllRejectedArticlesByUsername(string username);

        MyRejectedArticleViewModel GetMyRejectedArticleById(string articleId, string username);

        Article EditArticleAndResendForApproval(MyRejectedArticleViewModel model);
    }
}
