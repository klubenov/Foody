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
using Foody.Services.DataServices.Models.Articles;
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

        public Article CreateArticle(CreateArticleBindingModel createArticleBindingModel, string authorName)
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

                var imageLocation = this.imagesService.CreateImage(createArticleBindingModel.Image, this.GetType().Name.Replace("Service", string.Empty), articleId);

                this.context.Articles.First(a => a.Id == articleId).ImageLocation = imageLocation;
                article.ImageLocation = imageLocation;
                context.SaveChanges();
            }

            return article;
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

            if (article == null)
            {
                return null;
            }

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

        public bool RejectArticle(string articleId, string rejectComment, string rejectedBy)
        {
            var article = this.context.Articles.FirstOrDefault(a => a.Id == articleId);

            if (article == null || article.IsRejected)
            {
                return false;
            }

            article.IsRejected = true;
            article.IsSentForApproval = false;
            article.RejectedOn = DateTime.UtcNow;
            article.RejectComment = rejectComment;
            article.RejectedByUser = rejectedBy;

            context.SaveChanges();

            return true;
        }

        public AllMyApprovedArticlesViewModel GetAllApprovedArticlesByUsername(string username)
        {
            var myApprovedArticlesViewModels = this.context.Articles.Include(a => a.Author)
                .Where(a => a.Author.UserName == username && a.IsApproved == true)
                .Select(a => new MyApprovedArticlesListViewModel
                {
                    ArticleId = a.Id,
                    ApprovedOn = a.ApprovedOn ?? a.PostDate,
                    PostedOn = a.PostDate,
                    Title = a.Title
                }).OrderBy(a => a.PostedOn).ToList();

            var allMyApprovedArticlesViewModel = new AllMyApprovedArticlesViewModel
            {
                Items = myApprovedArticlesViewModels
            };

            allMyApprovedArticlesViewModel.PaginationModel.TotalPages =
                paginationService.GetTotalPages(allMyApprovedArticlesViewModel.Items.Count);

            return allMyApprovedArticlesViewModel;
        }

        public MyArticleViewModel GetMyArticleById(string articleId, string username)
        {
            var myArticleViewModel = this.context.Articles.Include(a => a.Author).Where(a => a.Id == articleId && a.Author.UserName == username).Select(a => 
                new MyArticleViewModel
                {
                    Title = a.Title,
                    Content = a.Content,
                    ApprovedOn = a.ApprovedOn,
                    PostedOn = a.PostDate,
                    ImageLocation = a.ImageLocation
                }).FirstOrDefault();

            return myArticleViewModel;
        }

        public AllMyRejectedArticlesViewModel GetAllRejectedArticlesByUsername(string username)
        {
            var myRejectedArticlesViewModels = this.context.Articles.Include(a => a.Author)
                .Where(a => a.Author.UserName == username && a.IsRejected == true)
                .Select(a => new MyRejectedArticlesListViewModel
                {
                    ArticleId = a.Id,
                    RejectedBy = a.RejectedByUser,
                    PostedOn = a.PostDate,
                    LastRejectOn = a.RejectedOn.Value,
                    Title = a.Title
                }).OrderBy(a => a.LastRejectOn).ToList();

            var allMyRejectedArticlesViewModel = new AllMyRejectedArticlesViewModel
            {
                Items = myRejectedArticlesViewModels
            };

            allMyRejectedArticlesViewModel.PaginationModel.TotalPages =
                paginationService.GetTotalPages(allMyRejectedArticlesViewModel.Items.Count);

            return allMyRejectedArticlesViewModel;
        }

        public MyRejectedArticleViewModel GetMyRejectedArticleById(string articleId, string username)
        {
            var myRejectedArticle = this.context.Articles.Include(a => a.Author)
                .Where(a => a.Id == articleId && a.Author.UserName == username && a.IsRejected).Select(a =>
                    new MyRejectedArticleViewModel
                    {
                        Id = a.Id,
                        Content = a.Content,
                        Title = a.Title,
                        ImageLocation = a.ImageLocation,
                        PostedOn = a.PostDate,
                        RejectedOn = a.RejectedOn,
                        RejectComment = a.RejectComment
                    }).FirstOrDefault();

            return myRejectedArticle;
        }

        public Article EditArticleAndResendForApproval(MyRejectedArticleViewModel model)
        {
            var article = this.context.Articles.FirstOrDefault(a => a.Id == model.Id);
            if (article == null)
            {
                return null;
            }

            article.Title = model.Title;
            article.Content = model.Content;
            article.IsRejected = false;
            article.IsSentForApproval = true;

            if (model.NewImage != null)
            {
                string newImageLocation = this.imagesService.RecreateImage(model.NewImage, this.GetType().Name.Replace("Service", string.Empty), model.ImageLocation, model.Id);
                article.ImageLocation = newImageLocation;
            }

            context.SaveChanges();
            return article;
        }
    }
}
