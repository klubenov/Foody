using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foody.Data;
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
    }
}
