using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastBudjet.Data.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(20, 2)")]
        public decimal Balance{ get; set; }

        public string Description { get; set; }

        public virtual IEnumerable<Transaction> Transactions { get; set; }
        public virtual IEnumerable<AccountHistory> AccountHistories { get; set; }

    }
}
