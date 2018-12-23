using System;
using System.Collections.Generic;
using System.Text;

namespace Foody.Services.Models.Shared
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string PageLink { get; set; }
    }
}
