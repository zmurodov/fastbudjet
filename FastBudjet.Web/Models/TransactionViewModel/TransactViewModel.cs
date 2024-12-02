using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Web.TransactionViewModel
{
    public class TransactViewModel
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool Income { get; set; }
        public string CreatedTime { get; set; }
        public string SendedOn { get; set; }

        public string AccountName { get; set; }
    }
}
