using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.DataServices.Common
{
    public class PaginationService : IPaginationService
    {
        public int GetTotalPages(int totalItems)
        {
            if (totalItems == 0)
            {
                return 1;
            }

            if (totalItems % Constants.ArticlesPageCount == 0)
            {
                return totalItems / Constants.ArticlesPageCount;
            }

            return totalItems / Constants.ArticlesPageCount + 1;
        }

        public TModel GetPageModel<TModel, TItem>(TModel model, int currentPage, string searchText, Type controllerType, string pageName, Type areaType = null) where TModel : IPaginateable<TItem>
        {
            if (currentPage > model.PaginationModel.TotalPages)
            {
                currentPage = model.PaginationModel.TotalPages;
            }

            if (currentPage < 1)
            {
                currentPage = 1;
            }

            model.PaginationModel.CurrentPage = currentPage;

            var pageArticles = new List<TItem>();

            for (int i = 0; i < Constants.ArticlesPageCount; i++)
            {
                var previousCounter = (currentPage - 1) * Constants.ArticlesPageCount + i;

                if (previousCounter == model.Items.Count)
                {
                    break;
                }

                pageArticles.Add(model.Items.Skip(previousCounter).First());
            }

            model.Items = pageArticles;

            var areaName = string.Empty;
            var pageLink = string.Empty;
            if (areaType != null)
            {
                var controllerAreaData =
                    controllerType.GetCustomAttributesData().First(ca => ca.AttributeType == areaType);
                areaName = controllerAreaData.ConstructorArguments.FirstOrDefault().Value.ToString();
            }

            var urlWithoutArea = $"/{controllerType.Name.Replace("Controller", string.Empty)}/{pageName}";
            if (!string.IsNullOrEmpty(areaName))
            {
                pageLink = $"/{areaName}{urlWithoutArea}";
            }
            else
            {
                pageLink = urlWithoutArea;
            }

            model.PaginationModel.PageLink = pageLink;
            model.PaginationModel.SearchText = searchText;

            return model;
        }
    }
}
