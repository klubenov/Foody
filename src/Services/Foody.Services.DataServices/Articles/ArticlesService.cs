using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Foody.Data;
using Foody.Data.Models;
using Foody.Data.Models.Publishing;
using Foody.Services.DataServices.Images;
using Foody.Services.Models.Articles;
using Microsoft.EntityFrameworkCore;

namespace Foody.Services.DataServices.Articles
{
    public class ArticlesService : IArticlesService
    {
        private readonly FoodyDbContext context;
        private readonly IImagesService imagesService;

        public ArticlesService(FoodyDbContext context, IImagesService imagesService)
        {
            this.context = context;
            this.imagesService = imagesService;
        }

        public void CreateArticle(CreateArticleBindingModel createArticleBindingModel, string authorName)
        {
            var author = this.context.Users.Single(u => u.UserName == authorName);
            var roleId = this.context.UserRoles.First(ur => ur.UserId == author.Id).RoleId;
            var role = this.context.Roles.First(r => r.Id == roleId).Name;
            bool isAuthorSuperAdmin = role == "Super-admin";


            var article = new Article
            {
                Title = createArticleBindingModel.Title,
                Content = createArticleBindingModel.Content,
                Author = author,
                PostDate = DateTime.UtcNow,
                IsApproved = isAuthorSuperAdmin,
                IsRejected = false,
                IsSentForApproval = !isAuthorSuperAdmin
            };

            this.context.Articles.Add(article);
            this.context.SaveChanges();

            if (createArticleBindingModel.Image != null)
            {
                var articleId = this.context.Articles.First(a => a.Title == createArticleBindingModel.Title).Id;

                var imageLocation = this.imagesService.CreateImage(createArticleBindingModel.Image, "Articles", articleId);

                this.context.Articles.First(a => a.Id == articleId).ImageLocation = imageLocation;
                context.SaveChanges();
            }
        }

        public AllArticlesForApprovalViewModel GetAllArticlesForApproval()
        {
            var articlesForApproval = this.context.Articles.Where(a => a.IsSentForApproval).Select(a =>
                new ArticleForApprovalListViewModel
                {
                    ArticleId = a.Id,
                    Title = a.Title,
                    Author = a.Author.UserName,
                    PostedOn = a.PostDate
                }).ToList();

            var allArticlesForApproval = new AllArticlesForApprovalViewModel
            {
                ArticleForApprovalListViewModels = articlesForApproval
            };

            return allArticlesForApproval;
        }

        public ArticleForApprovalViewModel GetArticleForApproval(string id)
        {
            var article = this.context.Articles.Include(a => a.Author).FirstOrDefault(a => a.Id == id);

            var articleForApproval = new ArticleForApprovalViewModel
            {
                Title = article.Title,
                Author = article.Author.UserName,
                Content = article.Content,
                ImageLocation = article.ImageLocation,
                PostedOn = article.PostDate
            };

            return articleForApproval;
        }
    }
}
