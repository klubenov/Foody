using System;
using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Publishing
{
    public class ArticleViewModel : IPaginateable<CommentViewModel>
    {
        public ArticleViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<CommentViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }

        public CommentBindingModel CommentBindingModel { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime PostedOn { get; set; }

        public string ImageLocation { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }
    }
}
