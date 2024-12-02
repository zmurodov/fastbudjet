using FastBudjet.Data.Models;
using FastBudjet.Web.Models.TransactionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastBudjet.Web.Models.AccountsViewModel;

namespace FastBudjet.Web.Models.HomeViewModel
{
    public class IndexViewModel
    {

        public TransactionViewModel.CreateViewModel CreateViewModel { get; set; }
        public MonthStatViewModel ThisMonthStat { get; set; }
        public MonthStatViewModel LastMonthStat { get; set; }
        public IEnumerable<AccountViewModel> Accounts { get; set; }
        public IEnumerable<Category> IncomingCategories { get; set; }
        public IEnumerable<Category> ExpenseCategories { get; set; }
        public IEnumerable<DailyStatViewModel> MonthDailyStats { get; set; }

        public string Summary { get; set; }



    }
}
