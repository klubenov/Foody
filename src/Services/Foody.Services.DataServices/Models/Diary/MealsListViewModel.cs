﻿using System;
using System.Collections.Generic;
using Foody.Services.DataServices.Models.Shared;

namespace Foody.Services.DataServices.Models.Diary
{
    public class MealsListViewModel : IPaginateable<MealForEditingListViewModel>
    {
        public MealsListViewModel()
        {
            this.PaginationModel = new PaginationModel();
        }

        public ICollection<MealForEditingListViewModel> Items { get; set; }

        public PaginationModel PaginationModel { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
