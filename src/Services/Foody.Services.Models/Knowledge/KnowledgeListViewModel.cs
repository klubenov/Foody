using System;
using System.Collections.Generic;
using System.Text;
using Foody.Services.Models.Shared;

namespace Foody.Services.Models.Knowledge
{
    public class KnowledgeListViewModel : IPaginateable<KnowledgeItemListViewModel>
    {
        public KnowledgeListViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<KnowledgeItemListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}
