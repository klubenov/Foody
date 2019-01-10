using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Common
{
    public interface IPaginationService
    {
        int GetTotalPages(int totalItems);

        TModel GetPageModel<TModel,TItem>(TModel model, int currentPage, string searchText, Type controllerType, string pageName, Type areaType)
            where TModel : IPaginateable<TItem>;
    }
}
