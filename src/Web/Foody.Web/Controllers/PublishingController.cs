using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Data.Models.Publishing;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Publishing;
using Foody.Services.Models.Publishing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Controllers
{
    public class PublishingController : Controller
    {
        private readonly IPublishingService publishingService;
        private readonly IPaginationService paginationService;

        public PublishingController(IPublishingService publishingService, IPaginationService paginationService)
        {
            this.publishingService = publishingService;
            this.paginationService = paginationService;
        }

        [HttpGet]
        public IActionResult Index(int currentPage = 1)
        {
            var model = this.publishingService.GetAllArticles();

            model = this.paginationService.GetPageModel<AllArticlesListViewModel, ArticleListViewModel>(model,
                currentPage, null, this.GetType(), "Index", null);

            return View(model);
        }

        [HttpGet]
        public IActionResult ViewArticle(string articleId, int currentPage)
        {
            var article = this.publishingService.GetArticle(articleId);

            if (article == null)
            {
                return RedirectToAction("Index", new {currentPage});
            }

            article = this.paginationService.GetPageModel<ArticleViewModel, CommentViewModel>(article, currentPage,
                articleId, this.GetType(), "ViewArticle", null);

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult PostComment(ArticleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.publishingService.SaveComment(model.CommentBindingModel, this.User.Identity.Name);
                return RedirectToAction("ViewArticle",
                    new {model.CommentBindingModel.ArticleId, model.PaginationModel.CurrentPage});
            }

            var article = this.publishingService.GetArticle(model.CommentBindingModel.ArticleId);

            if (article == null)
            {
                return RedirectToAction("Index", new { model.PaginationModel.CurrentPage });
            }

            article = this.paginationService.GetPageModel<ArticleViewModel, CommentViewModel>(article, model.PaginationModel.CurrentPage,
                article.Id, this.GetType(), "ViewArticle", null);

            return View("ViewArticle", article);
        }
    }
}