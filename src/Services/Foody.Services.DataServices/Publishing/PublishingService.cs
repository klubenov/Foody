using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foody.Data;
using Foody.Data.Models.Publishing;
using Foody.Services.DataServices.Common;
using Foody.Services.Models.Publishing;
using Microsoft.EntityFrameworkCore;

namespace Foody.Services.DataServices.Publishing
{
    public class PublishingService : IPublishingService
    {
        private readonly FoodyDbContext context;
        private readonly IPaginationService paginationService;

        public PublishingService(FoodyDbContext context, IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public AllArticlesListViewModel GetAllArticles()
        {
            var articles = this.context.Articles.Include(a => a.Author.UserName).Where(a => a.IsApproved).Select(a => new ArticleListViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Author = a.Author.UserName,
                PostedOn = a.ApprovedOn ?? a.PostDate
            }).ToList();

            var allArticlesViewModel = new AllArticlesListViewModel
            {
                Items = articles
            };

            allArticlesViewModel.PaginationModel.TotalPages =
                this.paginationService.GetTotalPages(allArticlesViewModel.Items.Count);

            return allArticlesViewModel;
        }

        public ArticleViewModel GetArticle(string articleId)
        {
            var article = this.context.Articles.Include(a => a.Author).Include(a => a.Comments)
                .ThenInclude(c => c.Author).Where(a => a.Id == articleId).Select(a => new ArticleViewModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Author = a.Author.UserName,
                    PostedOn = a.ApprovedOn ?? a.PostDate,
                    Content = a.Content,
                    ImageLocation = a.ImageLocation,
                    Items = a.Comments.Select(c => new CommentViewModel
                    {
                        Content = c.Content,
                        Author = c.Author.UserName,
                        PostedOn = c.PostDate
                    }).OrderByDescending(c => c.PostedOn).ToList()
                }).FirstOrDefault();

            if (article == null)
            {
                return null;
            }

            article.PaginationModel.TotalPages = this.paginationService.GetTotalPages(article.Items.Count);

            return article;
        }

        public Comment SaveComment(CommentBindingModel model, string username)
        {
            var author = this.context.Users.FirstOrDefault(u => u.UserName == username);

            if (author == null)
            {
                return null;
            }

            var comment = new Comment
            {
                ArticleId = model.ArticleId,
                Content = model.Content,
                PostDate = DateTime.UtcNow,
                Author = author
            };

            this.context.Comments.Add(comment);

            this.context.SaveChanges();

            return comment;
        }
    }
}
