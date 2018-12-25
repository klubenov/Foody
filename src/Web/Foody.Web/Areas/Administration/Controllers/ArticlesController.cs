using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Data.Models;
using Foody.Services.DataServices.Articles;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Menu;
using Foody.Services.Models;
using Foody.Services.Models.Articles;
using Foody.Services.Models.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Areas.Administration.Controllers
{
    [Area(AreaName)]
    public class ArticlesController : Controller
    {
        private const string AreaName = "Administration";
        private const string ApprovalErrorMessage = "The article you are trying to approve either does not exist, or it is aleary approved. Please choose another article from the list below.";
        private const string RejectionErrorMessage = "The article you are trying to reject either does not exist, or it is aleary rejected. Please choose another article from the list below.";
        private const string InitialOpenCheckString = "TruestOfTheTrue";

        private readonly IMenuService menuService;
        private readonly IArticlesService articlesService;
        private readonly IPaginationService paginationService;

        public ArticlesController(IMenuService menuService, IArticlesService articlesService, IPaginationService paginationService)
        {
            this.menuService = menuService;
            this.articlesService = articlesService;
            this.paginationService = paginationService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Super-admin")]
        public IActionResult ArticlesMenu()
        {
            var model = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, null, null);
            
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
        [ValidateAntiForgeryToken]
        public IActionResult Create_Article(CreateArticleBindingModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.articlesService.CreateArticle(model, this.User.Identity.Name);
                return RedirectToAction("ArticlesMenu");

            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Create_Article", model);
            return View("ArticlesMenu", reloadModel);
        }

        [HttpGet]
        [Authorize(Roles = "Super-admin")]
        public IActionResult Articles_Awaiting_Approval(int currentPage = 1, string initialOpen = InitialOpenCheckString, string errorMessage = null)
        {
            // TODO: Optimize pagination content gathering
            var model = this.articlesService.GetAllArticlesForApproval();

            model = this.paginationService.GetPageModel<AllArticlesForApprovalViewModel, ArticleForApprovalListViewModel>(
                    model, currentPage, this.GetType(), "Articles_Awaiting_Approval", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("Articles_Awaiting_Approval", model);
            }
            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "Articles_Awaiting_Approval", model);
                return View("ArticlesMenu", reloadModel);
            }

            return RedirectToAction("ArticlesMenu");
        }

        [HttpPost]
        [Authorize(Roles = "Super-admin")]
        [Route("/" + AreaName + "/ArticlesMenu/OpenForApproval")]
        [ValidateAntiForgeryToken]
        public IActionResult OpenForApproval(string articleId, int currentPage)
        {
            var article = articlesService.GetArticleForApproval(articleId);

            if (article == null)
            {
                return RedirectToAction("ArticlesMenu");
            }

            article.CurrentPage = currentPage;

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenForApproval", article);

            return View("ArticlesMenu", reloadModel);
        }

        [HttpPost]
        [Authorize(Roles = "Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(string articleId)
        {
            var approveResult = this.articlesService.ApproveArticle(articleId);

            if (approveResult == false)
            {
                return RedirectToAction("Articles_Awaiting_Approval",
                    new {currentPage = 1, initialOpen = false, errorMessage = ApprovalErrorMessage});
            }

            return RedirectToAction("Articles_Awaiting_Approval",
                new { currentPage = 1, initialOpen = false });
        }

        [HttpPost]
        [Authorize(Roles = "Super-admin")]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(string articleId, string rejectComment)
        {
            var rejectResult = this.articlesService.RejectArticle(articleId, rejectComment, this.User.Identity.Name);

            if (rejectResult == false)
            {
                return RedirectToAction("Articles_Awaiting_Approval",
                    new { currentPage = 1, initialOpen = false, errorMessage = RejectionErrorMessage });
            }

            return RedirectToAction("Articles_Awaiting_Approval",
                new { currentPage = 1, initialOpen = false });
        }

        [HttpGet]
        [Authorize(Roles = "Super-admin")]
        public IActionResult My_Articles(int currentPage = 1, string initialOpen = InitialOpenCheckString)
        {
            var model = this.articlesService.GetAllApprovedArticlesByUsername(this.User.Identity.Name);

            model = this.paginationService.GetPageModel<AllMyApprovedArticlesViewModel, MyApprovedArticlesListViewModel>(
                    model, currentPage, this.GetType(), "My_Articles", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("My_Articles", model);
            }
            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "My_Articles", model);
                return View("ArticlesMenu", reloadModel);
            }

            return RedirectToAction("ArticlesMenu");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult My_Approved_Articles(int currentPage = 1, string initialOpen = InitialOpenCheckString)
        {
            var model = this.articlesService.GetAllApprovedArticlesByUsername(this.User.Identity.Name);

            model = this.paginationService.GetPageModel<AllMyApprovedArticlesViewModel, MyApprovedArticlesListViewModel>(
                model, currentPage, this.GetType(), "My_Approved_Articles", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("My_Approved_Articles", model);
            }
            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "My_Approved_Articles", model);
                return View("ArticlesMenu", reloadModel);
            }

            return RedirectToAction("ArticlesMenu");
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Super-admin")]
        [ValidateAntiForgeryToken]
        [Route("/" + AreaName + "/ArticlesMenu/OpenMyArticle")]
        public IActionResult OpenMyArticle(string articleId, int currentPage, string sourceMethodName)
        {
            var article = this.articlesService.GetMyArticleById(articleId, this.User.Identity.Name);

            if (article == null)
            {
                return RedirectToAction("ArticlesMenu");
            }

            article.CurrentPage = currentPage;
            article.SourceMethodName = sourceMethodName;

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenMyArticle", article);
            return View("ArticlesMenu", reloadModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult My_Rejected_Articles(int currentPage = 1, string initialOpen = InitialOpenCheckString)
        {
            var model = this.articlesService.GetAllRejectedArticlesByUsername(this.User.Identity.Name);

            model = this.paginationService.GetPageModel<AllMyRejectedArticlesViewModel, MyRejectedArticlesListViewModel>(
                    model, currentPage, this.GetType(), "My_Rejected_Articles", typeof(AreaAttribute));

            if (initialOpen == InitialOpenCheckString)
            {
                return PartialView("My_Rejected_Articles", model);
            }

            else if (initialOpen == "false")
            {
                var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "My_Rejected_Articles", model);
                return View("ArticlesMenu", reloadModel);
            }

            return RedirectToAction("ArticlesMenu");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult OpenMyRejectedArticle(string articleId, int currentPage)
        {
            var article = this.articlesService.GetMyRejectedArticleById(articleId, this.User.Identity.Name);

            if (article == null)
            {
                return RedirectToAction("ArticlesMenu");
            }

            article.CurrentPage = currentPage;

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenMyRejectedArticle", article);
            return View("ArticlesMenu", reloadModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public IActionResult ResendForApproval(MyRejectedArticleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.articlesService.EditArticleAndResendForApproval(model);
                return RedirectToAction("ArticlesMenu");
            }

            var reloadModel = this.menuService.GetMenuItems(this.GetType(), typeof(HttpGetAttribute), typeof(AuthorizeAttribute), AreaName, "OpenMyRejectedArticle", model);
            return View("ArticlesMenu", reloadModel);
        }
    }
}