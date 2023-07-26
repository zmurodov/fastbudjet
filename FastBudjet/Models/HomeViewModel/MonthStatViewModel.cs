using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Models.HomeViewModel
{
    public class MonthStatViewModel
    {
        public decimal IncomeNum { get; set; }
        public decimal ExpenseNum { get; set; }
        public string Income { get; set; }
        public string Expense { get; set; }

        public string Diff { get; set; }
    }
}
