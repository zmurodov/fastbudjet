using FastBudjet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastBudjet.Web.TransactionViewModel;

namespace FastBudjet.Web.Models.TransactionViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Category> IncomingCategories { get; set; }
        public IEnumerable<Category> ExpenseCategories { get; set; }
        public IEnumerable<Account> Accounts{ get; set; }
        public IEnumerable<TransactViewModel> Transactions{ get; set; }

        public CreateViewModel CreateViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }

        public UpdateViewModel UpdateViewModel { get; set; }

        public string Summary { get; set; }

    }
}
