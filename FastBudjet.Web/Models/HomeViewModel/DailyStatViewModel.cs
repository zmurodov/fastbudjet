using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Web.Models.HomeViewModel
{
    public class DailyStatViewModel
    {
        public decimal Summary { get; set; }
        public string SummaryStr { get; set; }
        public string Day { get; set; } 
    }
}
