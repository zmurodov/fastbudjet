using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Web.Models.AccountsViewModel
{
    public class CreateViewModel
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string? Description { get; set; }
    }
}
