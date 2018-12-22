using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Foody.Data;
using Foody.Data.Models;
using Foody.Data.Models.Publishing;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Images;
using Foody.Services.Models.Articles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Foody.Services.DataServices.Articles
{
    public class ArticlesService : IArticlesService
    {
        private readonly FoodyDbContext context;
        private readonly IImagesService imagesService;
        private readonly IPaginationService paginationService;

        public ArticlesService(FoodyDbContext context, IImagesService imagesService, IPaginationService paginationService)
        {
            this.context = context;
            this.imagesService = imagesService;
            this.paginationService = paginationService;
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
                ApprovedOn = DateTime.UtcNow,
                IsApproved = isAuthorSuperAdmin,
                IsRejected = false,
                IsSentForApproval = !isAuthorSuperAdmin
            };

            this.context.Articles.Add(article);
            this.context.SaveChanges();

            if (createArticleBindingModel.Image != null)
            {
                var articleId = this.context.Articles.First(a => a.Title == createArticleBindingModel.Title).Id;

                var imageLocation = this.imagesService.CreateImage(createArticleBindingModel.Image, this.GetType().Name.Replace("Service", string.Empty), articleId);

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
                }).OrderBy(a => a.PostedOn).ToList();

            var allArticlesForApproval = new AllArticlesForApprovalViewModel
            {
                Items = articlesForApproval
            };


            allArticlesForApproval.PaginationModel.TotalPages = paginationService.GetTotalPages(allArticlesForApproval.Items.Count());

            return allArticlesForApproval;
        }

        public ArticleForApprovalViewModel GetArticleForApproval(string id)
        {
            var article = this.context.Articles.Include(a => a.Author).FirstOrDefault(a => a.Id == id);

            var articleForApproval = new ArticleForApprovalViewModel
            {
                Id = id,
                Title = article.Title,
                Author = article.Author.UserName,
                Content = article.Content,
                ImageLocation = article.ImageLocation,
                PostedOn = article.PostDate
            };

            return articleForApproval;
        }

        public bool ApproveArticle(string articleId)
        {
            var article = this.context.Articles.FirstOrDefault(a => a.Id == articleId);

            if (article == null || article.IsApproved)
            {
                return false;
            }

            article.IsApproved = true;
            article.IsSentForApproval = false;
            article.ApprovedOn = DateTime.UtcNow;

            context.SaveChanges();

            return true;
        }

        public bool RejectArticle(string articleId, string rejectComment)
        {
            var article = this.context.Articles.FirstOrDefault(a => a.Id == articleId);

            if (article == null || article.IsRejected)
            {
                return false;
            }

            article.IsRejected = true;
            article.IsSentForApproval = false;

            context.SaveChanges();

            return true;
        }
    }
}
