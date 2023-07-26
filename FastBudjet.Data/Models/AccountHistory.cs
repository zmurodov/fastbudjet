using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastBudjet.Data.Models
{
    public class AccountHistory
    {
        public int Id { get; set; }
        [Required]
        public int AccountId { get; set; }

        [Required]
        public int TransactionId { get; set; }

        [Required]
        [Column(TypeName = "decimal(20, 2)")]
        public decimal Summary { get; set; }

        public virtual Account  Account{ get; set; }
        public virtual Transaction  Transaction { get; set; }
    }
}
