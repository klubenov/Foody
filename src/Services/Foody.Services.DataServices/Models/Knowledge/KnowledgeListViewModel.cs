using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Knowledge
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
