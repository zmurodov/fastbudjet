﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastBudjet.Web.Models.AccountsViewModel
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BalanceStr { get; set; }
        public string Description { get; set; }
    }
}
