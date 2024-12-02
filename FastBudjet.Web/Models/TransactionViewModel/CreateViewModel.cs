using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Web.Models.TransactionViewModel
{
    public class CreateViewModel
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int AccountId { get; set; }
        public bool Income { get; set; }
        public string CreatedTime { get; set; }
        public string SendedOn { get; set; }
    }
}
