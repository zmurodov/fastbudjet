using FastBudjet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Models.AccountsViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<Account> Accounts{ get; set; }

        public CreateViewModel CreateViewModel{ get; set; } 
        public UpdateViewModel UpdateViewModel{ get; set; } 
        public string Summary { get; set; } 
    }
}
