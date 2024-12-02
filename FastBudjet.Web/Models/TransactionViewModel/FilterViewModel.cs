using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Web.Models.TransactionViewModel
{
    public class FilterViewModel
    {

        [Display(Name = "StartDate")]
        public string StartDate { get; set; }

        [Display(Name = "EndDate")]
        public string EndDate { get; set; }

        [Display(Name = "TransactionType")]
        public string TransactionType { get; set; }
        public int? AccountId { get; set; }
    }
}
