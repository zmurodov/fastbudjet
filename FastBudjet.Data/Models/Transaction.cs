using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text;

namespace FastBudjet.Data.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(20, 2)")]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int AccountId { get; set; }

        [Required]
        public bool Income { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime SendedOn { get; set; }
        public virtual Category Category { get; set; }
        public virtual Account Account { get; set; }
        public virtual IEnumerable<AccountHistory> AccountHistories { get; set; }

    }
}
