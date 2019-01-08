using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Foody.Services.Models.Diary
{
    public class DiaryIndexViewModel
    {
        public StatisticsViewModel HomePageStatistics { get; set; }

        public StatisticsViewModel CustomPeriodStatistics { get; set; }

        [Required(ErrorMessage = "Please set beginning of the period..")]
        public DateTime StartCustomDate { get; set; }

        [Required(ErrorMessage = "Please set end of the period..")]
        public DateTime EndCustomDate { get; set; }

        public bool InitialOpen { get; set; }
    }
}
