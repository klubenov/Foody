using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Data.Models;
using Foody.Services.DataServices.Articles;
using Foody.Services.DataServices.Menu;
using Foody.Services.Models;
using Foody.Services.Models.Articles;
using Foody.Services.Models.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ArticlesController : Controller
    {
        private readonly IMenuService menuService;
        private readonly IArticlesService articlesService;

        public ArticlesController(IMenuService menuService, IArticlesService articlesService)
        {
            this.menuService = menuService;
            this.articlesService = articlesService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult ArticlesMenu()
        {
            var model = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), "Administration", null, null);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Create_Article()
        {
            return PartialView("Create_Article");
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult Create_Article(CreateArticleBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.articlesService.CreateArticle(model, this.User.Identity.Name);
                return RedirectToAction("ArticlesMenu");

            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), "Administration", "Create_Article", model);
            return View("ArticlesMenu", reloadModel);
        }

        [HttpGet]
        [Authorize(Roles = "Super-admin")]
        public IActionResult Articles_Awaiting_Approval()
        {
            var model = this.articlesService.GetAllArticlesForApproval();

            return PartialView("Articles_Awaiting_Approval", model);
        }

        
        [Authorize(Roles = "Super-admin")]
        public IActionResult OpenForApproval(string articleId)
        {
            var article = articlesService.GetArticleForApproval(articleId);

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), "Administration", "OpenForApproval", article);

            return View("ArticlesMenu", reloadModel);
        }
    }
}