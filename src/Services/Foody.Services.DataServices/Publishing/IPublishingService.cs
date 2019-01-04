using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Publishing;

namespace Foody.Services.DataServices.Publishing
{
    public interface IPublishingService
    {
        AllArticlesListViewModel GetAllArticles();
    }
}
