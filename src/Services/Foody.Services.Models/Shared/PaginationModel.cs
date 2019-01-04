﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foody.Services.Models.Shared
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string PageLink { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 character allowed")]
        [RegularExpression("^[\\dA-Za-z\\s]+$", ErrorMessage = "Only digits, letters and whitespaces allowed!")]
        public string SearchText { get; set; }
    }
}
