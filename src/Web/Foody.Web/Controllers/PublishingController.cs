using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Publishing;
using Foody.Services.Models.Publishing;
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
    }
}