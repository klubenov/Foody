using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foody.Services.DataServices.Common;
using Foody.Services.DataServices.Knowledge;
using Foody.Services.DataServices.Models.Knowledge;
using Microsoft.AspNetCore.Mvc;

namespace Foody.Web.Controllers
{
    public class KnowledgeController : Controller
    {
        private readonly IKnowledgeService knowledgeService;
        private readonly IPaginationService paginationService;

        public KnowledgeController(IKnowledgeService knowledgeService, IPaginationService paginationService)
        {
            this.knowledgeService = knowledgeService;
            this.paginationService = paginationService;
        }

        [HttpGet]
        public IActionResult Index(KnowledgeListViewModel model, int currentPage = 1, string searchText = null)
        {
            if (string.IsNullOrEmpty(model.PaginationModel.SearchText))
            {
                model.PaginationModel.SearchText = searchText;
            }
            var isModelValid = TryValidateModel(model);
            if (isModelValid)
            {
                var itemListModel = this.knowledgeService.GetItemList(model);
                itemListModel =
                    this.paginationService.GetPageModel<KnowledgeListViewModel, KnowledgeItemListViewModel>(itemListModel,
                        currentPage, model.PaginationModel.SearchText, this.GetType(), "Index", null);
                return View(itemListModel);
            }

            return this.View(model);
        }

        [HttpGet]
        public IActionResult ViewItem(string itemId, string currentPage, string searchText)
        {
            var itemModel = this.knowledgeService.GetItem(itemId);

            if (itemModel == null)
            {
                return RedirectToAction("Index", new {currentPage, searchText});
            }

            itemModel.CurrentPage = currentPage;
            itemModel.SearchText = searchText;

            return View(itemModel);
        }
    }
}