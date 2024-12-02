using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastBudjet.Web.TransactionViewModel;

namespace FastBudjet.Web.Models.TransactionViewModel
{
    public class ResultViewModel
    {
        public IEnumerable<TransactViewModel> Transactions{ get; set; }
        public string Summary { get; set; }
    }
}
