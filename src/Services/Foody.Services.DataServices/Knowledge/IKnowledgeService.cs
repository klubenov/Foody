using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Knowledge;

namespace Foody.Services.DataServices.Knowledge
{
    public interface IKnowledgeService
    {
        KnowledgeListViewModel GetItemList(KnowledgeListViewModel model);

        KnowledgeItemViewModel GetItem(string itemId);
    }
}
